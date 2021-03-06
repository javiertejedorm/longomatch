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
using LongoMatch.Core.Common;
using LongoMatch.Core.Store;
using LongoMatch.Gui.Menus;
using System.Collections.Generic;

namespace LongoMatch.Gui.Component
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class PlaysPositionViewer : Gtk.Bin
	{
	
		PlaysMenu menu;
		Project project;

		public PlaysPositionViewer ()
		{
			this.Build ();
			field.Tagger.FieldPosition = FieldPositionType.Field;
			hfield.Tagger.FieldPosition = FieldPositionType.HalfField;
			goal.Tagger.FieldPosition = FieldPositionType.Goal;
			field.Tagger.ShowMenuEvent += HandleShowMenuEvent;
			hfield.Tagger.ShowMenuEvent += HandleShowMenuEvent;
			goal.Tagger.ShowMenuEvent += HandleShowMenuEvent;
			Config.EventsBroker.EventLoadedEvent += HandlePlayLoaded;
			menu = new PlaysMenu ();
		}

		public void LoadProject (Project project, EventsFilter filter)
		{
			this.project = project;
			if (project != null) {
				field.Tagger.Project = project;
				hfield.Tagger.Project = project;
				goal.Tagger.Project = project;
				field.Tagger.Background = project.GetBackground (FieldPositionType.Field);
				hfield.Tagger.Background = project.GetBackground (FieldPositionType.HalfField);
				goal.Tagger.Background = project.GetBackground (FieldPositionType.Goal);
				field.Tagger.Plays = project.Timeline;
				hfield.Tagger.Plays = project.Timeline;
				goal.Tagger.Plays = project.Timeline;
				field.Tagger.Filter = filter;
				hfield.Tagger.Filter = filter;
				goal.Tagger.Filter = filter;
			}
		}

		public void AddPlay (TimelineEvent play)
		{
			field.Tagger.AddPlay (play);
			hfield.Tagger.AddPlay (play);
			goal.Tagger.AddPlay (play);
			QueueDraw ();
		}

		public void RemovePlays (List<TimelineEvent> plays)
		{
			field.Tagger.RemovePlays (plays);
			hfield.Tagger.RemovePlays (plays);
			goal.Tagger.RemovePlays (plays);
			QueueDraw ();
		}

		void HandlePlayLoaded (TimelineEvent play)
		{
			if (play != null) {
				field.Tagger.SelectPlay (play);
				hfield.Tagger.SelectPlay (play);
				goal.Tagger.SelectPlay (play);
			} else {
				field.Tagger.ClearSelection ();
				hfield.Tagger.ClearSelection ();
				goal.Tagger.ClearSelection ();
			}
		}

		void HandleShowMenuEvent (List<TimelineEvent> plays)
		{
			if (plays == null || plays.Count == 0) {
				return;
			}
			menu.ShowMenu (project, plays);
		}

		protected override void OnDestroyed ()
		{
			field.Destroy ();
			hfield.Destroy ();
			goal.Destroy ();
			Config.EventsBroker.EventLoadedEvent -= HandlePlayLoaded;
			base.OnDestroyed ();
		}
	}
}

