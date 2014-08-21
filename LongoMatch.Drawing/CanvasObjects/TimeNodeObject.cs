//
//  Copyright (C) 2014 Andoni Morales Alastruey
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301, USA.
//
using System;
using LongoMatch.Store;
using LongoMatch.Interfaces.Drawing;
using LongoMatch.Interfaces;
using LongoMatch.Common;
using LongoMatch.Store.Drawables;
using System.IO;

namespace LongoMatch.Drawing.CanvasObjects
{
	public class TimeNodeObject: CanvasObject, ICanvasSelectableObject
	{
		ISurface needle;
		const int MAX_TIME_SPAN = 1000;

		public TimeNodeObject (TimeNode node)
		{
			TimeNode = node;
			SelectWhole = true;
		}
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (needle != null) {
				needle.Dispose ();
				needle = null;
			}
		}

		public TimeNode TimeNode {
			get;
			set;
		}

		public bool SelectWhole {
			get;
			set;
		}

		public Time MaxTime {
			set;
			protected get;
		}

		public double OffsetY {
			get;
			set;
		}
		
		public double Height {
			get {
				return StyleConf.TimelineCategoryHeight;
			}
		}

		public double SecondsPerPixel {
			set;
			protected get;
		}

		protected double StartX {
			get {
				return Utils.TimeToPos (TimeNode.Start, SecondsPerPixel);
			}
		}

		protected double StopX {
			get {
				return Utils.TimeToPos (TimeNode.Stop, SecondsPerPixel);
			}
		}

		protected double CenterX {
			get {
				return Utils.TimeToPos (TimeNode.Start + TimeNode.Duration / 2,
				                        SecondsPerPixel);
			}
		}

		public Selection GetSelection (Point point, double precision, bool inMotion=false)
		{
			double accuracy;
			if (point.Y >= OffsetY && point.Y < OffsetY + Height) {
				if (Drawable.MatchAxis (point.X, StartX, precision, out accuracy)) {
					return new Selection (this, SelectionPosition.Left, accuracy);
				} else if (Drawable.MatchAxis (point.X, StopX, precision, out accuracy)) {
					return new Selection (this, SelectionPosition.Right, accuracy);
				} else if (SelectWhole && point.X > StartX && point.X < StopX) {
					return new Selection (this, SelectionPosition.All,
					                      Math.Abs (CenterX - point.X));
				}
			}
			return null;
		}

		public void Move (Selection sel, Point p, Point start)
		{
			Time newTime = Utils.PosToTime (p, SecondsPerPixel);

			if (p.X < 0) {
				p.X = 0;
			} else if (newTime > MaxTime) {
				p.X = Utils.TimeToPos (MaxTime, SecondsPerPixel);
			}
			newTime = Utils.PosToTime (p, SecondsPerPixel);

			switch (sel.Position) {
			case SelectionPosition.Left:
				{
					if (newTime.MSeconds + MAX_TIME_SPAN > TimeNode.Stop.MSeconds) {
						TimeNode.Start.MSeconds = TimeNode.Stop.MSeconds - MAX_TIME_SPAN;
					} else {
						TimeNode.Start = newTime;
					}
					break;
				}
			case SelectionPosition.Right:
				{
					if (newTime.MSeconds - MAX_TIME_SPAN < TimeNode.Start.MSeconds) {
						TimeNode.Stop.MSeconds = TimeNode.Start.MSeconds + MAX_TIME_SPAN;
					} else {
						TimeNode.Stop = newTime;
					}
					break;
				}
			}
		}

		public override void Draw (IDrawingToolkit tk, Area area)
		{
			double linepos;

			tk.Begin ();
			if (needle == null) {
				string  path = Path.Combine (Config.IconsDir, StyleConf.TimelineNeedleUP); 
				Image img = Image.LoadFromFile (path);
				needle = tk.CreateSurface (img.Width, img.Height, img);
			}
			
			tk.FillColor = Config.Style.PaletteBackgroundLight;
			tk.StrokeColor = Config.Style.PaletteBackgroundLight;
			tk.LineWidth = StyleConf.TimelineLineSize;
			
			linepos = OffsetY + Height - StyleConf.TimelineLineSize;
			
			tk.DrawLine (new Point (StartX, linepos),
			             new Point (StopX, linepos));
			tk.DrawSurface (needle, new Point (StartX - needle.Width / 2, linepos - 9)); 
			tk.DrawSurface (needle, new Point (StopX - needle.Width / 2, linepos - 9)); 

			tk.FontSize = 16;
			tk.FontWeight = FontWeight.Bold;
			tk.FillColor = Config.Style.PaletteActive;
			tk.StrokeColor = Config.Style.PaletteActive;
			tk.DrawText (new Point (StartX, OffsetY), StopX - StartX,
			             Height - StyleConf.TimelineLineSize,
			             TimeNode.Name);
			tk.End ();
		}
	}
}
