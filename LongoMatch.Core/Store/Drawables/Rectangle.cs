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
using LongoMatch.Common;
using Newtonsoft.Json;

namespace LongoMatch.Store.Drawables
{
	public class Rectangle: Quadrilateral
	{
		public Rectangle ()
		{
		}
		
		public Rectangle (Point origin, double width, double height)
		{
			BottomLeft = origin;
			BottomRight = new Point (origin.X + width, origin.Y);
			TopLeft = new Point (origin.X, origin.Y + height);
			TopRight = new Point (origin.X + width, origin.Y + height);
		}
		
		[JsonIgnore]
		public double Width {
			get {
				return TopRight.X - TopLeft.X;
			}
		}
		
		[JsonIgnore]
		public double Height {
			get {
				return TopLeft.Y - BottomLeft.Y;
			}
		}
		
		[JsonIgnore]
		public Point Center {
			get {
				return new Point (BottomLeft.X + Width / 2, BottomLeft.Y + Height / 2);
			}
		}
		
		public override Selection GetSelection (Point p, double pr=0.05) {
			Selection selection;
			double d;
			
			selection = base.GetSelection (p, pr);
			
			if (selection.Position == SelectionPosition.All) {
				if (MatchAxis (p.X, TopLeft.X, pr, out d)) {
					return new Selection (this, SelectionPosition.Left, d);
				} else if (MatchAxis (p.X, TopRight.X, pr, out d)) {
					return new Selection (this, SelectionPosition.Right, d);
				} else if (MatchAxis (p.Y, TopLeft.Y, pr, out d)) {
					return new Selection (this, SelectionPosition.Top, d);
				} else if (MatchAxis (p.Y, BottomLeft.Y, pr, out d)) {
					return new Selection(this, SelectionPosition.Bottom, d);
				}
			}
			return selection;
		}
		
		public override void Move (Selection sel, Point p, Point moveStart) {
			switch (sel.Position) {
			case SelectionPosition.Left:
				TopLeft.X = BottomLeft.X = p.X;
				break;
			case SelectionPosition.Right:
				TopRight.X = BottomRight.X = p.X;
				break;
			case SelectionPosition.Top:
				TopLeft.Y = TopRight.Y = p.Y;
				break;
			case SelectionPosition.Bottom:
				BottomLeft.Y = BottomRight.Y = p.Y;
				break;
			case SelectionPosition.TopLeft:
				TopLeft = p;
				TopRight.Y = p.Y;
				BottomLeft.X = p.X;
				break;
			case SelectionPosition.TopRight:
				TopRight = p;
				TopLeft.Y = p.Y;
				BottomRight.X = p.X;
				break;
			case SelectionPosition.BottomLeft:
				BottomLeft = p;
				BottomRight.Y = p.Y;
				TopLeft.X = p.X;
				break;
			case SelectionPosition.BottomRight:
				BottomRight = p;
				BottomLeft.Y = p.Y;
				TopRight.X = p.X;
				break;
			case SelectionPosition.All:
				Center.X += p.X - moveStart.X;
				Center.Y += p.Y - moveStart.Y;
				break;
			}
		}
	}
}