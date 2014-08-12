// PlayListWidget.cs
//
//  Copyright (C) 2007-2009 Andoni Morales Alastruey
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301, USA.
//
//
using System.Linq;
using System.Collections.Generic;
using Gtk;
using LongoMatch.Common;
using LongoMatch.Handlers;
using LongoMatch.Interfaces;
using LongoMatch.Store.Playlists;
using LongoMatch.Store;
using Mono.Unix;

namespace LongoMatch.Gui.Component
{
	[System.ComponentModel.Category("LongoMatch")]
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PlayListWidget : Gtk.Bin
	{
		Project project;

		public PlayListWidget ()
		{
			this.Build ();
			playlisttreeview1.Reorderable = true;
			playlisttreeview1.RowActivated += HandleRowActivated;
			newbutton.CanFocus = false;
			newvideobutton.CanFocus = false;
			Config.EventsBroker.PlaylistsChangedEvent += HandlePlaylistsChangedEvent;
			Config.EventsBroker.PlaylistElementSelectedEvent += HandlePlaylistElementSelectedEvent;
		}

		protected override void OnDestroyed ()
		{
			Config.EventsBroker.PlaylistsChangedEvent -= HandlePlaylistsChangedEvent;
			Config.EventsBroker.PlaylistElementSelectedEvent -= HandlePlaylistElementSelectedEvent;
			base.OnDestroyed ();
		}

		void HandlePlaylistsChangedEvent (object sender)
		{
			if (sender != playlisttreeview1)
				playlisttreeview1.Reload ();
		}

		void HandlePlaylistElementSelectedEvent (Playlist playlist, IPlaylistElement element)
		{
			playlisttreeview1.QueueDraw ();
		}

		void HandleRowActivated (object o, RowActivatedArgs args)
		{
				TreeIter iter;
				Playlist playlist;
				IPlaylistElement element;
				
				playlisttreeview1.Model.GetIterFromString (out iter, args.Path.ToString ());
				var el = playlisttreeview1.Model.GetValue (iter, 0);
				if (el is Playlist) {
					playlist = el as Playlist;
					element = playlist.Elements.FirstOrDefault ();
				} else {
					TreeIter parent;
					playlisttreeview1.Model.IterParent (out parent, iter);
					playlist = playlisttreeview1.Model.GetValue (parent, 0) as Playlist;
					element = el as IPlaylistElement;
				}
			Config.EventsBroker.EmitPlaylistElementSelected (playlist, element);
		}

		public Project Project {
			set {
				project = value;
				playlisttreeview1.Project = value;
			}
			get {
				return project;
			}
		}

		protected virtual void OnNewbuttonClicked (object sender, System.EventArgs e)
		{
			Config.EventsBroker.EmitNewPlaylist (Project);
		}

		protected virtual void OnNewvideobuttonClicked (object sender, System.EventArgs ea)
		{
			Menu menu;

			menu = new Menu ();
			foreach (Playlist playlist in Project.Playlists) {
				MenuItem plmenu = new MenuItem (playlist.Name);
				plmenu.Activated += (s, e) => {
					Config.EventsBroker.EmitRenderPlaylist (playlist);
				};
				menu.Append (plmenu);
			}
			menu.ShowAll ();
			menu.Popup ();
		}
	}
}
