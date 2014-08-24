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
using Gtk;
using LongoMatch.Common;
using System.Collections.Generic;
using LongoMatch.Store.Templates;
using Gdk;

namespace LongoMatch.Gui.Component
{
	[System.ComponentModel.Category("LongoMatch")]
	[System.ComponentModel.ToolboxItem(true)]
	public class TeamsComboBox: Gtk.ComboBox
	{
		ListStore store;
		Button internalButton;
		CellRendererPixbuf pixrender;
		CellRendererText texrender;
		
		public TeamsComboBox ()
		{
		}

		public void Load (List<TeamTemplate> teams)
		{
			Clear ();
			pixrender = new CellRendererPixbuf ();
			texrender = new CellRendererText ();
			texrender.Font = StyleConf.NewTeamsFont;
			texrender.Alignment = Pango.Alignment.Center;

			if (Direction == TextDirection.Ltr) {
				PackStart (pixrender, false);
				PackEnd (texrender, true);
			} else {
				PackEnd (pixrender, false);
				PackStart (texrender, true);
			}
			
			store = new ListStore (typeof(Pixbuf), typeof(string), typeof(TeamTemplate));
			foreach (TeamTemplate t in teams) {
				Pixbuf shield;
				int size = StyleConf.NewTeamsIconSize;

				if (t.Shield == null) {
					shield = IconTheme.Default.LoadIcon (Constants.LOGO_ICON, size,
					                                     IconLookupFlags.ForceSvg);
				} else {
					shield = t.Shield.Scale (size, size).Value;
				}
				store.AppendValues (shield, t.TeamName, t);
			}
			SetAttributes (texrender, "text", 1);
			SetAttributes (pixrender, "pixbuf", 0);
			Model = store;
		
		} 

		public TeamTemplate ActiveTeam {
			get {
				TreeIter iter;

				GetActiveIter (out iter);
				return store.GetValue (iter, 2) as TeamTemplate;
			}
		}
	}
	
	[System.ComponentModel.Category("LongoMatch")]
	[System.ComponentModel.ToolboxItem(true)]
	public class HomeTeamsComboBox: TeamsComboBox
	{
		public HomeTeamsComboBox () {
			Direction = TextDirection.Rtl;
		}
	}
	
	[System.ComponentModel.Category("LongoMatch")]
	[System.ComponentModel.ToolboxItem(true)]
	public class AwayTeamsComboBox: TeamsComboBox
	{
		public AwayTeamsComboBox () {
			Direction = TextDirection.Ltr;
		}
	}
}

