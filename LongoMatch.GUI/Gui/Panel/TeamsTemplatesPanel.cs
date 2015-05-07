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
using Gdk;
using Gtk;
using LongoMatch.Core.Common;
using LongoMatch.Core.Handlers;
using LongoMatch.Core.Interfaces;
using LongoMatch.Core.Interfaces.GUI;
using LongoMatch.Core.Store.Templates;
using LongoMatch.Gui.Helpers;
using Mono.Unix;
using Pango;
using LongoMatch.Gui.Dialog;
using Image = LongoMatch.Core.Common.Image;

namespace LongoMatch.Gui.Panel
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class TeamsTemplatesPanel : Gtk.Bin, IPanel
	{
		public event BackEventHandle BackEvent;

		ListStore teams;
		Team loadedTeam;
		ITeamTemplatesProvider provider;
		TreeIter selectedIter;
		List<string> templatesNames;

		public TeamsTemplatesPanel ()
		{
			this.Build ();
			provider = Config.TeamTemplatesProvider;
			teamimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-team-header", 45, IconLookupFlags.ForceSvg);
			playerheaderimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-player-header", 45, IconLookupFlags.ForceSvg);
			newteamimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-team-add", 34, IconLookupFlags.ForceSvg);
			deleteteamimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-team-delete", 34, IconLookupFlags.ForceSvg);
			saveteamimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-team-save", 34, IconLookupFlags.ForceSvg);
			newplayerimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-player-add", 34, IconLookupFlags.ForceSvg);
			deleteplayerimage.Pixbuf = Helpers.Misc.LoadIcon ("longomatch-player-delete", 34, IconLookupFlags.ForceSvg);
			vseparatorimage.Pixbuf = Helpers.Misc.LoadIcon ("vertical-separator", 34, IconLookupFlags.ForceSvg);

			newteambutton.Entered += HandleEnterTeamButton;
			newteambutton.Left += HandleLeftTeamButton;
			newteambutton.Clicked += HandleNewTeamClicked;
			deleteteambutton.Entered += HandleEnterTeamButton;
			deleteteambutton.Left += HandleLeftTeamButton;
			deleteteambutton.Clicked += HandleDeleteTeamClicked;
			saveteambutton.Entered += HandleEnterTeamButton;
			saveteambutton.Left += HandleLeftTeamButton;
			saveteambutton.Clicked += (s, e) => {
				PromptSave (false);
			};
			newplayerbutton1.Entered += HandleEnterPlayerButton;
			newplayerbutton1.Left += HandleLeftPlayerButton;
			newplayerbutton1.Clicked += (object sender, EventArgs e) => {
				teamtemplateeditor1.AddPlayer ();
			};
			deleteplayerbutton.Entered += HandleEnterPlayerButton;
			deleteplayerbutton.Left += HandleLeftPlayerButton;
			deleteplayerbutton.Clicked += (object sender, EventArgs e) => {
				teamtemplateeditor1.DeleteSelectedPlayers ();
			};

			teams = new ListStore (typeof(Pixbuf), typeof(string), typeof(string));
			
			var cell = new CellRendererText ();
			cell.Editable = true;
			cell.Edited += HandleEdited;
			teamseditortreeview.Model = teams;
			teamseditortreeview.HeadersVisible = false;
			teamseditortreeview.AppendColumn ("Icon", new CellRendererPixbuf (), "pixbuf", 0); 
			teamseditortreeview.AppendColumn ("Text", cell, "text", 1); 
			teamseditortreeview.SearchColumn = 1;
			teamseditortreeview.EnableGridLines = TreeViewGridLines.None;
			teamseditortreeview.CursorChanged += HandleSelectionChanged;
			
			teamsvbox.WidthRequest = 280;
			
			teamtemplateeditor1.Visible = false;
			newteambutton.Visible = true;
			deleteteambutton.Visible = false;
			
			teamtemplateeditor1.VisibleButtons = false;

			panelheader1.ApplyVisible = false;
			panelheader1.Title = Catalog.GetString ("TEAMS MANAGER");
			panelheader1.BackClicked += (sender, o) => {
				PromptSave (true);
				if (BackEvent != null)
					BackEvent ();
			};
			
			editteamslabel.ModifyFont (FontDescription.FromString (Config.Style.Font + " 9"));
			editplayerslabel.ModifyFont (FontDescription.FromString (Config.Style.Font + " 9"));

			Load (null);
		}

		public override void Destroy ()
		{
			teamtemplateeditor1.Destroy ();
			base.Destroy ();
		}

		void Load (string templateName)
		{
			TreeIter templateIter = TreeIter.Zero;
			bool first = true;

			templatesNames = new List<string> ();
			teams.Clear ();
			foreach (Team template in provider.Templates) {
				Pixbuf img;
				TreeIter iter;
				string name = template.Name;

				if (template.Shield != null) {
					img = template.Shield.Scale (StyleConf.TeamsShieldIconSize,
						StyleConf.TeamsShieldIconSize).Value;
				} else {
					img = Helpers.Misc.LoadIcon ("longomatch-default-shield",
						StyleConf.TeamsShieldIconSize);
				}
				if (template.Static) {
					name += " (" + Catalog.GetString ("System") + ")";
				} else {
					templatesNames.Add (name);
				}
				iter = teams.AppendValues (img, name, template.Name);
				if (first || template.Name == templateName) {
					templateIter = iter;
				}
				first = false;
			}
			if (teams.IterIsValid (templateIter)) {
				teamseditortreeview.Selection.SelectIter (templateIter);
				HandleSelectionChanged (null, null);
			}
		}

		bool SaveTemplate (Team template)
		{
			try {
				provider.Update (template);
				return true;
			} catch (InvalidTemplateFilenameException ex) {
				Config.GUIToolkit.ErrorMessage (ex.Message, this);
				return false;
			}
		}

		void HandleEnterTeamButton (object sender, EventArgs e)
		{
			if (sender == newteambutton) {
				editteamslabel.Markup = Catalog.GetString ("New team");
			} else if (sender == deleteteambutton) {
				editteamslabel.Markup = Catalog.GetString ("Delete team");
			} else if (sender == saveteambutton) {
				editteamslabel.Markup = Catalog.GetString ("Save team");
			}
		}

		void HandleLeftTeamButton (object sender, EventArgs e)
		{
			editteamslabel.Markup = Catalog.GetString ("Manage teams");
		}

		void HandleEnterPlayerButton (object sender, EventArgs e)
		{
			if (sender == newplayerbutton1) {
				editplayerslabel.Markup = Catalog.GetString ("New player");
			} else if (sender == deleteplayerbutton) {
				editplayerslabel.Markup = Catalog.GetString ("Delete player");
			}
		}

		void HandleLeftPlayerButton (object sender, EventArgs e)
		{
			editplayerslabel.Markup = Catalog.GetString ("Manage players");
		}

		void SaveLoadedTeam ()
		{
			if (loadedTeam == null)
				return;

			if (!SaveTemplate (loadedTeam)) {
				return;
			}
			/* The shield might have changed, update it just in case */
			if (loadedTeam.Shield != null) {
				TreeIter iter;
				
				teams.GetIterFirst (out iter);
				while (teams.IterIsValid (iter)) {
					string name = teams.GetValue (iter, 2) as string;
					if (name == loadedTeam.Name) {
						Pixbuf shield = loadedTeam.Shield.Scale (StyleConf.TeamsShieldIconSize,
							                StyleConf.TeamsShieldIconSize).Value;
						teamseditortreeview.Model.SetValue (iter, 0, shield);
						break;
					}
					teams.IterNext (ref iter);
				}
			}
			teamtemplateeditor1.Edited = false;
		}

		void SaveStatic ()
		{
			string msg = Catalog.GetString ("System teams can't be edited, do you want to create a copy?");
			if (Config.GUIToolkit.QuestionMessage (msg, null, this)) {
				string newName;
				while (true) {
					newName = Config.GUIToolkit.QueryMessage (Catalog.GetString ("Name:"), null,
						loadedTeam.Name + "_copy", this);
					if (newName == null)
						break;
					if (templatesNames.Contains (newName)) {
						msg = Catalog.GetString ("A team with the same name already exists"); 
						Config.GUIToolkit.ErrorMessage (msg, this);
					} else {
						break;
					}
				}
				if (newName == null) {
					return;
				}
				Team newTeam = loadedTeam.Clone ();
				newTeam.ID = Guid.NewGuid ();
				newTeam.Name = newName;
				newTeam.Static = false;
				if (SaveTemplate (newTeam)) {
					Load (newTeam.Name);
				}
			}
		}

		void PromptSave (bool prompt)
		{
			if (loadedTeam != null && teamtemplateeditor1.Edited) {
				if (loadedTeam.Static) {
					if (!prompt) {
						SaveStatic ();
					}
				} else if (prompt) {
					string msg = Catalog.GetString ("Do you want to save the current template");
					if (Config.GUIToolkit.QuestionMessage (msg, null, this)) {
						SaveLoadedTeam ();
					}
				} else {
					SaveLoadedTeam ();
				}
			}
		}

		void LoadTeam (Team team)
		{
			PromptSave (true);
			
			loadedTeam = team;
			team.TemplateEditorMode = true;
			teamtemplateeditor1.Team = loadedTeam;
		}

		void HandleSelectionChanged (object sender, EventArgs e)
		{
			Team selected;

			teamseditortreeview.Selection.GetSelected (out selectedIter);
			try {
				selected = Config.TeamTemplatesProvider.Load (teams.GetValue (selectedIter, 2) as string);
			} catch (Exception ex) {
				Log.Exception (ex);
				Config.GUIToolkit.ErrorMessage (Catalog.GetString ("Could not load team"));
				return;
			}
			deleteteambutton.Visible = selected != null;
			teamtemplateeditor1.Visible = selected != null;
			if (selected != null) {
				LoadTeam (selected);
			}
		}

		void HandleDeleteTeamClicked (object sender, EventArgs e)
		{
			if (loadedTeam != null) {
				if (loadedTeam.Static) {
					string msg = Catalog.GetString ("System teams can't be deleted");
					MessagesHelpers.WarningMessage (this, msg);
					return;
				} else {
					string msg = Catalog.GetString ("Do you really want to delete the template: ") + loadedTeam.Name;
					if (MessagesHelpers.QuestionMessage (this, msg, null)) {
						provider.Delete (loadedTeam.Name);
						teams.Remove (ref selectedIter);
						templatesNames.Remove (loadedTeam.Name);
						selectedIter = TreeIter.Zero;
						teamseditortreeview.Selection.SelectPath (new TreePath ("0"));
						HandleSelectionChanged (null, null);
					}
				}
			}
		}

		void HandleNewTeamClicked (object sender, EventArgs e)
		{
			bool create = false;
			bool force = false;
			
			EntryDialog dialog = new EntryDialog (Toplevel as Gtk.Window);
			dialog.ShowCount = true;
			dialog.Text = Catalog.GetString ("New team");
			dialog.AvailableTemplates = templatesNames;
			
			while (dialog.Run () == (int)ResponseType.Ok) {
				if (dialog.Text == "") {
					MessagesHelpers.ErrorMessage (dialog, Catalog.GetString ("The template name is empty."));
					continue;
				} else if (dialog.Text == "default") {
					MessagesHelpers.ErrorMessage (dialog, Catalog.GetString ("The template can't be named 'default'."));
					continue;
				} else if (provider.Exists (dialog.Text)) {
					var msg = Catalog.GetString ("The template already exists. " +
					          "Do you want to overwrite it?");
					if (MessagesHelpers.QuestionMessage (this, msg)) {
						create = true;
						force = true;
						break;
					}
				} else {
					create = true;
					break;
				}
			}
			
			if (create) {
				if (force) {
					try {
						provider.Delete (dialog.Text);
					} catch (Exception ex) {
						Log.Exception (ex);
					}
				}
				if (dialog.SelectedTemplate != null) {
					provider.Copy (dialog.SelectedTemplate, dialog.Text);
				} else {
					Team team;
					team = Team.DefaultTemplate (dialog.Count);
					team.TeamName = dialog.Text;
					team.Name = dialog.Text;
					if (!SaveTemplate (team)) {
						dialog.Destroy ();
						return;
					}
				}
				Load (dialog.Text);
			}
			dialog.Destroy ();
		}

		void HandleEdited (object o, EditedArgs args)
		{
			TreeIter iter;
			teams.GetIter (out iter, new TreePath (args.Path));
 
			string name = (string)teams.GetValue (iter, 2);
			if (name != args.NewText) {
				if (templatesNames.Contains (args.NewText)) {
					Config.GUIToolkit.ErrorMessage (
						Catalog.GetString ("A team with the same name already exists"), this);
					args.RetVal = false;
				} else {
					try {
						Team team = provider.Load (name);
						team.Name = args.NewText;
						provider.Save (team);
						provider.Delete (name);
						templatesNames.Remove (name);
						templatesNames.Add (team.Name);
						teams.SetValue (iter, 1, args.NewText);
						teams.SetValue (iter, 2, args.NewText);
					} catch (Exception ex) {
						Config.GUIToolkit.ErrorMessage (ex.Message);
					}
				}
			}
		}
	}
}

