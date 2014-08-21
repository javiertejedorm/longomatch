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
using LongoMatch.Interfaces.Drawing;
using LongoMatch.Common;
using System.Collections.Generic;
using LongoMatch.Store.Drawables;

namespace LongoMatch.Drawing.CanvasObjects
{
	public class BenchObject: CanvasObject, ICanvasSelectableObject
	{
		public BenchObject ()
		{
			BenchPlayers = new List<PlayerObject> ();
		}

		public List<PlayerObject> BenchPlayers {
			get;
			set;
		}

		public bool SubstitutionMode {
			get;
			set;
		}
		
		public Point Position {
			get;
			set;
		}

		public double Width {
			get;
			set;
		}

		public double Height {
			get;
			set;
		}

		public int PlayersPerRow {
			get;
			set;
		}

		public int PlayersSize {
			get;
			set;
		}
		
		public void Update ()
		{
			if (BenchPlayers == null) {
				return;
			}
			for (int i = 0; i < BenchPlayers.Count; i++) {
				PlayerObject po;
				double x, y;
				double s = Width / PlayersPerRow;
				
				x = s * (i % PlayersPerRow) + s / 2;
				y = s * (i / PlayersPerRow) + s / 2;

				po = BenchPlayers [i];
				po.Position = new Point (x, y);
				po.Size = PlayersSize;
			}
		}

		public override void Draw (IDrawingToolkit tk, Area area)
		{
			if (BenchPlayers == null) {
				return;
			}
			tk.Begin ();
			tk.TranslateAndScale (Position, new Point (1, 1)); 
			tk.LineStyle = LineStyle.Dashed;
			tk.LineWidth = Config.Style.BenchLineWidth;
			tk.StrokeColor = Config.Style.PaletteActive;
			tk.FillColor = null;
			tk.DrawRectangle (new Point (0, 0), Width, Height);
			tk.LineStyle = LineStyle.Normal;

			foreach (PlayerObject po in BenchPlayers) {
				po.Playing = false;
				po.SubstitutionMode = SubstitutionMode;
				po.Draw (tk, area);
			}
			
			tk.End ();
		}

		public Selection GetSelection (Point point, double precision, bool inMotion=false)
		{
			Selection selection = null;

			if (BenchPlayers == null) {
				return selection;
			}
			
			point = Utils.ToUserCoords (point, Position, 1, 1);
			
			foreach (PlayerObject po in BenchPlayers) {
				selection = po.GetSelection (point, precision);
				if (selection != null)
					break;
			}
			return selection;
		}

		public void Move (Selection s, Point p, Point start)
		{
		}
	}
}

