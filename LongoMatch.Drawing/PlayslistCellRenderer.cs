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
using LongoMatch.Store;
using LongoMatch.Store.Playlists;

namespace LongoMatch.Drawing
{
	public class PlayslistCellRenderer
	{
	
		public static void RenderSeparationLine (IDrawingToolkit tk, IContext context, Area backgroundArea)
		{
			
			double x1, x2, y;
			
			x1 = backgroundArea.Start.X;
			x2 = x1 + backgroundArea.Width;
			y = backgroundArea.Start.Y + backgroundArea.Height; 
			tk.LineWidth = 1;
			tk.StrokeColor = Config.Style.PaletteBackgroundLight;
			tk.DrawLine (new Point (x1, y), new Point (x2, y));
		}

		static void RenderCount (Color color, int count, IDrawingToolkit tk, Area backgroundArea, Area cellArea)
		{
			double countX1, countX2, countY, countYC;
			
			countX1 = cellArea.Start.X + cellArea.Width - StyleConf.ListImageWidth + StyleConf.ListCountRadio;
			countX2 = countX1 + StyleConf.ListCountWidth;
			countYC = backgroundArea.Start.Y + backgroundArea.Height / 2;
			countY = countYC - StyleConf.ListCountRadio;
			tk.LineWidth = 0;
			tk.FillColor = color;
			tk.DrawCircle (new Point (countX1, countYC), StyleConf.ListCountRadio);
			tk.DrawCircle (new Point (countX2, countYC), StyleConf.ListCountRadio);
			tk.DrawRectangle (new Point (countX1, countY), StyleConf.ListCountWidth, 2 * StyleConf.ListCountRadio);
			tk.StrokeColor = Config.Style.PaletteBackgroundDark;
			tk.FontAlignment = FontAlignment.Center;
			tk.DrawText (new Point (countX1, countY), StyleConf.ListCountWidth, 2 * StyleConf.ListCountRadio, count.ToString ());
		}

		static void RenderBackgroundAndText (bool isExpanded, IDrawingToolkit tk, Area backgroundArea, Point textP, double textW, string text)
		{
			Color textColor, backgroundColor;

			/* Background */
			tk.LineWidth = 0;
			if (isExpanded) {
				backgroundColor = Config.Style.PaletteBackgroundLight;
				textColor = Config.Style.PaletteSelected;
			}
			else {
				backgroundColor = Config.Style.PaletteBackground;
				textColor = Config.Style.PaletteWidgets;
			}
			tk.FillColor = backgroundColor;
			tk.DrawRectangle (backgroundArea.Start, backgroundArea.Width, backgroundArea.Height);

			/* Text */
			tk.StrokeColor = textColor;
			tk.FontSize = 14;
			tk.FontWeight = FontWeight.Bold;
			tk.FontAlignment = FontAlignment.Left;
			tk.DrawText (textP, textW, backgroundArea.Height, text);
		}

		public static void RenderPlayer (Player player, int count, bool isExpanded, IDrawingToolkit tk,
		                               IContext context, Area backgroundArea, Area cellArea)
		{
			Point image, text;
			double textWidth;

			image = new Point (cellArea.Start.X + 10, cellArea.Start.Y);
			text = new Point (image.X + StyleConf.ListImageWidth, cellArea.Start.Y);
			textWidth = cellArea.Start.X + cellArea.Width - text.X;

			tk.Context = context;
			tk.Begin ();
			RenderBackgroundAndText (isExpanded, tk, backgroundArea, text, textWidth, player.ToString());
			/* Photo */
			if (player.Photo != null) {
				tk.DrawImage (image, StyleConf.ListImageWidth, backgroundArea.Height, player.Photo, true); 
			}
			RenderCount (Config.Style.PaletteActive, count, tk, backgroundArea, cellArea);
			RenderSeparationLine (tk, context, backgroundArea);
			tk.End ();
		}

		public static void RenderPlaylist (Playlist playlist, int count, bool isExpanded, IDrawingToolkit tk,
		                                   IContext context, Area backgroundArea, Area cellArea)
		{
			tk.Context = context;
			tk.Begin ();
			RenderBackgroundAndText (isExpanded, tk, backgroundArea, cellArea.Start, cellArea.Width, playlist.Name);
			RenderCount (Config.Style.PaletteActive, count, tk, backgroundArea, cellArea);
			RenderSeparationLine (tk, context, backgroundArea);
			tk.End ();
		}

		public static void RenderAnalysisCategory (AnalysisCategory cat, int count, bool isExpanded, IDrawingToolkit tk,
		                                           IContext context, Area backgroundArea, Area cellArea)
		{
			tk.Context = context;
			tk.Begin ();
			RenderBackgroundAndText (isExpanded, tk, backgroundArea, cellArea.Start, cellArea.Width, cat.Name);
			RenderCount (cat.Color, count, tk, backgroundArea, cellArea);
			RenderSeparationLine (tk, context, backgroundArea);
			tk.End ();
		}
		
		public static void RenderPlay (Color color, Image ss, bool selected, string desc,
		                               int count, bool isExpanded, IDrawingToolkit tk,
		                               IContext context, Area backgroundArea, Area cellArea, CellState state)
		{
			Point selectPoint, textPoint, imagePoint, circlePoint;
			double textWidth;
			
			
			selectPoint = new Point (backgroundArea.Start.X, backgroundArea.Start.Y);
			textPoint = new Point (selectPoint.X + StyleConf.ListSelectedWidth + 10, selectPoint.Y);
			imagePoint = new Point (backgroundArea.Start.X + backgroundArea.Width - StyleConf.ListImageWidth - 10,
			                        selectPoint.Y);
			textWidth = imagePoint.X - textPoint.X; 
			circlePoint = new Point (selectPoint.X + StyleConf.ListSelectedWidth / 2,
			                         selectPoint.Y + backgroundArea.Height / 2);

			tk.Context = context;
			tk.Begin ();
			
			tk.LineWidth = 0;
			if (state.HasFlag (CellState.Prelit)) {
				tk.FillColor = Config.Style.PaletteBackgroundLight;
			} else if (state.HasFlag (CellState.Selected)) {
				tk.FillColor = Config.Style.PaletteBackground;
			} else {
				tk.FillColor = Config.Style.PaletteBackgroundDark;
			}
			tk.DrawRectangle (backgroundArea.Start, backgroundArea.Width, backgroundArea.Height);

			/* Selection rectangle */
			tk.LineWidth = 0;
			tk.FillColor = color;
			tk.DrawRectangle (selectPoint, StyleConf.ListSelectedWidth, backgroundArea.Height);
			tk.FillColor = Config.Style.PaletteBackgroundDark;
			tk.DrawCircle (circlePoint, (StyleConf.ListSelectedWidth / 2) - 1); 
			if (selected) {
				tk.FillColor = Config.Style.PaletteActive;
				tk.DrawCircle (circlePoint, (StyleConf.ListSelectedWidth / 2) - 2); 
			}

			tk.FontSize = 10;
			tk.FontWeight = FontWeight.Normal;
			tk.StrokeColor = Config.Style.PaletteSelected;
			tk.FontAlignment = FontAlignment.Left;
			tk.DrawText (textPoint, textWidth, cellArea.Height, desc);
			
			if (ss != null) {
				tk.DrawImage (imagePoint, StyleConf.ListImageWidth, cellArea.Height, ss, true);
			}
			RenderSeparationLine (tk, context, backgroundArea);
			tk.End ();
		}
		
		public static void Render (object item, int count, bool isExpanded, IDrawingToolkit tk,
		                           IContext context, Area backgroundArea, Area cellArea, CellState state)
		{
			if (item is AnalysisCategory) {
				RenderAnalysisCategory (item as AnalysisCategory, count, isExpanded, tk,
				                        context, backgroundArea, cellArea);
			} else if (item is Play) {
				Play p = item as Play;
				RenderPlay (p.Category.Color, p.Miniature, p.Selected, p.Description, count, isExpanded, tk,
				            context, backgroundArea, cellArea, state);
			} else if (item is Player) {
				RenderPlayer (item as Player, count, isExpanded, tk, context, backgroundArea, cellArea);
			} else if (item is Playlist) {
				RenderPlaylist (item as Playlist, count, isExpanded, tk, context, backgroundArea, cellArea);
			} else if (item is PlaylistPlayElement) {
				PlaylistPlayElement p = item as PlaylistPlayElement;
				RenderPlay (p.Play.Category.Color, p.Miniature, p.Selected, p.Description, count, isExpanded, tk,
				            context, backgroundArea, cellArea, state);
			}
		}
	}
}
