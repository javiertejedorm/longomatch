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
using LongoMatch.Handlers;
using LongoMatch.Common;

namespace LongoMatch.Gui.Panel
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WelcomePanel : Gtk.Bin
	{
		public WelcomePanel ()
		{
			this.Build ();
			backgroundwidget.Background = Gdk.Pixbuf.LoadFromResource (Constants.BACKGROUND);
		}
		
		public void Bind (MainWindow window)
		{
			openbutton.Clicked += (sender, e) => {window.EmitOpenProject ();};
			newbutton.Clicked += (sender, e) => {window.EmitNewProject ();};
			teamsbutton.Clicked += (sender, e) => {window.EmitManageTeams ();};
			sportsbutton.Clicked += (sender, e) => {window.EmitManageCategories ();};
		    preferencesbutton.Clicked += (sender, e) => {window.EmitEditPreferences ();};
		    projectsbutton.Clicked += (sender, e) =>  {window.EmitManageProjects ();};
		}
	}
}
