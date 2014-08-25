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
using System.Collections.Generic;
using Gtk;
using LongoMatch.Handlers;
using LongoMatch.Gui;
using LongoMatch.Store;
using LongoMatch.Interfaces.GUI;

namespace LongoMatch.Gui.Panel
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class OpenProjectPanel : Gtk.Bin, IPanel
	{
		public event BackEventHandle BackEvent;

		public OpenProjectPanel ()
		{
			this.Build ();
			
			projectlistwidget.ProjectSelected += HandleProjectSelected;
			projectlistwidget.SelectionMode = SelectionMode.Single;
			panelheader1.ApplyVisible = false;
			panelheader1.BackClicked += HandleClicked;
			panelheader1.Title = "OPEN PROJECT";
		}

		public List<ProjectDescription> Projects{
			set {
				projectlistwidget.Fill (value);
			}
		}
		
		void HandleClicked (object sender, EventArgs e)
		{
			if (BackEvent != null)
				BackEvent ();
		}

		void HandleProjectSelected (ProjectDescription project)
		{
			Config.EventsBroker.EmitOpenProjectID  (project.ID);
		}
	}
}

