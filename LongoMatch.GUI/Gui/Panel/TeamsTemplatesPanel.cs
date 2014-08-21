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
using Mono.Unix;
using Stetic;

using Image = LongoMatch.Common.Image;
using LongoMatch.Common;
using LongoMatch.Gui.Dialog;
using LongoMatch.Interfaces;
using LongoMatch.Store;
using LongoMatch.Store.Templates;
using LongoMatch.Gui.Helpers;
using LongoMatch.Handlers;
using LongoMatch.Interfaces.GUI;

namespace LongoMatch.Gui.Panel
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TeamsTemplatesPanel : Gtk.Bin, IPanel
	{
		public event BackEventHandle BackEvent;
		
		ListStore teams;
		List<string> selectedTeams;
		TeamTemplate loadedTeam;
		Dictionary<string, TreeIter> itersDict;
		
		ITeamTemplatesProvider provider;
		
		public TeamsTemplatesPanel ()
		{
			this.Build ();
			provider = Config.TeamTemplatesProvider;
			logoimage.Pixbuf = IconTheme.Default.LoadIcon ("longomatch", 80, IconLookupFlags.ForceSvg);
			teamimage.Pixbuf = IconTheme.Default.LoadIcon ("longomatch-team-header", 80, IconLookupFlags.ForceSvg);
			newteamimage.Pixbuf = IconTheme.Default.LoadIcon ("longomatch-team-add", 40, IconLookupFlags.ForceSvg);
			deleteteamimage.Pixbuf = IconTheme.Default.LoadIcon ("longomatch-team-delete", 40, IconLookupFlags.ForceSvg);
			saveteamimage.Pixbuf = IconTheme.Default.LoadIcon ("longomatch-team-save", 40, IconLookupFlags.ForceSvg);
			
			teams = new ListStore (typeof(Pixbuf), typeof(string), typeof (string));
			itersDict = new Dictionary<string, TreeIter>();
			
			teamseditortreeview.Model = teams;
			teamseditortreeview.HeadersVisible = false;
			teamseditortreeview.AppendColumn ("Icon", new CellRendererPixbuf (), "pixbuf", 0); 
			teamseditortreeview.AppendColumn ("Text", new CellRendererText (), "text", 1); 
			teamseditortreeview.SearchColumn = 1;
			teamseditortreeview.TooltipColumn = 2;
			teamseditortreeview.EnableGridLines = TreeViewGridLines.None;
			teamseditortreeview.CursorChanged += HandleSelectionChanged;
			
			teamsvbox.WidthRequest = 280;
			
			teamtemplateeditor1.Visible = false;
			newteamtemplatebutton.Visible = true;
			deleteteamteamplatebutton.Visible = false;
			
			selectedTeams = new List<string>();
			newteamtemplatebutton.Clicked += HandleNewTeamClicked;
			deleteteamteamplatebutton.Clicked += HandleDeleteTeamClicked;
			teamtemplateeditor1.TemplateSaved += (s, e) => {SaveLoadedTeam ();};
			
			backrectbutton.Clicked += (sender, o) => {
				PromptSave ();
				if (BackEvent != null)
					BackEvent();
			};
			Load (null);
			
		}

		void Load (string templateName)
		{
			TreeIter templateIter = TreeIter.Zero;
			bool first = true;
			
			teams.Clear ();
			itersDict.Clear ();
			foreach (TeamTemplate template in provider.Templates) {
				Pixbuf img;
				TreeIter iter;
				
				if (template.Shield != null) {
					img = template.Shield.Value;
				} else {
					img = IconTheme.Default.LoadIcon (Constants.LOGO_ICON,
					                                  Constants.MAX_SHIELD_ICON_SIZE,
					                                  IconLookupFlags.ForceSvg);
				}
				iter = teams.AppendValues (img, template.Name, template.TeamName);
				itersDict.Add (template.Name, iter);
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
		
		void SaveLoadedTeam () {
			if (loadedTeam == null)
				return;

			provider.Update (loadedTeam);
			/* The shield might have changed, update it just in case */
			if (loadedTeam.Shield != null) {
				teamseditortreeview.Model.SetValue (itersDict[loadedTeam.Name], 0,
				                              loadedTeam.Shield.Value);
			}
		}
		
		void PromptSave () {
			if (loadedTeam != null) {
				if (teamtemplateeditor1.Edited) {
					string msg = Catalog.GetString ("Do you want to save the current template");
					if (Config.GUIToolkit.QuestionMessage (msg, null, this)) {
						SaveLoadedTeam ();
					}
				}
			}
		}
		
		void LoadTeam (string teamName) {
			PromptSave ();
			
			try  {
				loadedTeam = provider.Load (teamName);
				teamtemplateeditor1.Team = loadedTeam;
			} catch (Exception ex) {
				Log.Exception (ex);
				GUIToolkit.Instance.ErrorMessage (Catalog.GetString ("Could not load template"));
				return;
			}
		}
		
		void HandleSelectionChanged (object sender, EventArgs e)
		{
			TreeIter iter;
			TreePath[] pathArray;
			
			selectedTeams.Clear ();

			pathArray = teamseditortreeview.Selection.GetSelectedRows ();
			for(int i=0; i< pathArray.Length; i++) {
				teamseditortreeview.Model.GetIterFromString (out iter, pathArray[i].ToString());
				selectedTeams.Add (teamseditortreeview.Model.GetValue (iter, 1) as string);
			}
			
			deleteteamteamplatebutton.Visible = selectedTeams.Count >= 1;
			teamtemplateeditor1.Visible = true;
			
			if (selectedTeams.Count == 1) {
				LoadTeam (selectedTeams[0]);
			}
		}
		
		void HandleDeleteTeamClicked (object sender, EventArgs e)
		{
			foreach (string teamName in selectedTeams) {
				if (teamName == "default") {
					MessagesHelpers.ErrorMessage (this,
						Catalog.GetString ("The default template can't be deleted"));
					continue;
				}
				string msg = Catalog.GetString("Do you really want to delete the template: ") + teamName;
				if (MessagesHelpers.QuestionMessage (this, msg, null)) {
					provider.Delete (teamName);
				}
			}
			Load ("default");
		}

		void HandleNewTeamClicked (object sender, EventArgs e)
		{
			bool create = false;
			
			EntryDialog dialog = new EntryDialog();
			dialog.TransientFor = (Gtk.Window)this.Toplevel;
			dialog.ShowCount = true;
			dialog.Text = Catalog.GetString("New team");
			dialog.AvailableTemplates = provider.TemplatesNames;
			
			while (dialog.Run() == (int)ResponseType.Ok) {
				if (dialog.Text == "") {
					MessagesHelpers.ErrorMessage (dialog, Catalog.GetString("The template name is empty."));
					continue;
				} else if (dialog.Text == "default") {
					MessagesHelpers.ErrorMessage (dialog, Catalog.GetString("The template can't be named 'default'."));
					continue;
				} else if(provider.Exists(dialog.Text)) {
					var msg = Catalog.GetString("The template already exists. " +
					                            "Do you want to overwrite it ?");
					if (MessagesHelpers.QuestionMessage (this, msg)) {
						create = true;
						break;
					}
				} else {
					create = true;
					break;
				}
			}
			
			if (create) {
				if (dialog.SelectedTemplate != null) {
					provider.Copy (dialog.SelectedTemplate, dialog.Text);
				} else {
					TeamTemplate team;
					team = TeamTemplate.DefaultTemplate (dialog.Count);
					team.TeamName = dialog.Text;
					team.Name = dialog.Text;
					provider.Update (team);
				}
				Load (dialog.Text);
			}
			dialog.Destroy();
		}
	}
}

