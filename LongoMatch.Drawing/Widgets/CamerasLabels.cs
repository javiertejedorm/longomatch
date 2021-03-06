﻿//
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
using System.Linq;
using LongoMatch.Core.Common;
using LongoMatch.Core.Interfaces.Drawing;
using LongoMatch.Core.Store;
using LongoMatch.Drawing.CanvasObjects.Timeline;
using Mono.Unix;

namespace LongoMatch.Drawing.Widgets
{
	public class CamerasLabels: Canvas
	{
		MediaFileSet fileSet;

		public CamerasLabels (IWidget widget) : base (widget)
		{
		}

		public double Scroll {
			set {
				foreach (var o in Objects) {
					LabelObject cl = o as LabelObject;
					cl.Scroll = value; 
				}
			}
		}

		public void Load (MediaFileSet fileSet)
		{
			ClearObjects ();
			this.fileSet = fileSet;
			FillCanvas ();
			widget.ReDraw ();
		}

		void AddLabel (LabelObject label)
		{
			Objects.Add (label);
		}

		void FillCanvas ()
		{
			LabelObject l;
			int i = 0, w, h;
			double requiredWidth;

			w = StyleConf.TimelineLabelsWidth * 2;
			h = StyleConf.TimelineCameraHeight;
			widget.Width = w;

			// Main camera
			l = new CameraLabelObject (w, h, i * h) {
				Name = fileSet [0].Name,
				BackgroundColor = Config.Style.PaletteBackgroundLight
			};
			AddLabel (l);
			i++;

			// Periods
			l = new CameraLabelObject (w, h, i * h) {
				Name = Catalog.GetString ("Periods"),
				BackgroundColor = Config.Style.PaletteBackgroundLight
			};
			AddLabel (l);
			i++;

			// Secondary cams
			for (int j = 1; j < fileSet.Count; j++) {
				l = new CameraLabelObject (w, h, i * h) {
					Name = fileSet [j].Name,
					BackgroundColor = Config.Style.PaletteBackground
				};
				AddLabel (l);
				i++;
			}

			requiredWidth = Objects.Max (la => (la as LabelObject).RequiredWidth);
			foreach (LabelObject label in Objects) {
				label.Width = requiredWidth;
			}
			widget.Width = requiredWidth;
		}

		public override void Draw (IContext context, Area area)
		{
			tk.Context = context;
			tk.Begin ();
			tk.Clear (Config.Style.PaletteBackground);
			tk.End ();

			base.Draw (context, area);
		}
	}
}

