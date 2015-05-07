// MainWindow.cs
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
using System;
using System.Collections.Generic;
using Gdk;
using Gtk;
using LongoMatch.Core.Common;
using LongoMatch.Gui.Dialog;
using LongoMatch.Core.Interfaces.GUI;
using LongoMatch.Core.Store;
using LongoMatch.Gui.Component;
using LongoMatch.Gui.Panel;

namespace LongoMatch.Gui
{
	[System.ComponentModel.Category ("LongoMatch")]
	[System.ComponentModel.ToolboxItem (false)]
	public partial class MainWindow : Gtk.Window, IMainController
	{
		IGUIToolkit guiToolKit;
		IAnalysisWindow analysisWindow;
		Project openedProject;
		ProjectType projectType;
		Widget currentPanel;
		Widget stackPanel;

		#region Constructors

		public MainWindow (IGUIToolkit guiToolkit) :
			base (Constants.SOFTWARE_NAME)
		{
			this.Build ();
			this.guiToolKit = guiToolkit;
			Title = Constants.SOFTWARE_NAME;
			projectType = ProjectType.None;
			
			ConnectSignals ();
			ConnectMenuSignals ();

			// Default screen
			Screen screen = Display.Default.DefaultScreen;
			// Which monitor is our window on
			int monitor = screen.GetMonitorAtWindow (this.GdkWindow);
			// Monitor size
			Rectangle monitor_geometry = screen.GetMonitorGeometry (monitor);
			// Resize to a convenient size
			this.Resize (monitor_geometry.Width * 80 / 100, monitor_geometry.Height * 80 / 100);
			if (Utils.RunningPlatform () == PlatformID.MacOSX) {
				this.Move (monitor_geometry.Width * 10 / 100, monitor_geometry.Height * 10 / 100);
			}
		}

		#endregion

		#region Plubic Methods

		public IRenderingStateBar RenderingStateBar {
			get {
				return renderingstatebar1;
			}
		}

		public MenuShell Menu {
			get {
				return menubar1;
			}
		}

		public MenuItem QuitMenu {
			get {
				return (MenuItem)this.UIManager.GetWidget ("/menubar1/FileAction/QuitAction");
			}
		}

		public MenuItem PreferencesMenu {
			get {
				return (MenuItem)this.UIManager.GetWidget ("/menubar1/FileAction/PreferencesAction");
			}
		}

		public MenuItem AboutMenu {
			get {
				return (MenuItem)this.UIManager.GetWidget ("/menubar1/HelpAction/AboutAction");
			}
		}

		public void SetPanel (Widget panel)
		{
			if (panel == null) {
				ResetGUI ();
			} else {
				if (currentPanel is IAnalysisWindow && panel is PreferencesPanel) {
					RemovePanel (true);
				} else {
					RemovePanel (false);
				}
				currentPanel = panel;
				panel.Show ();
				if (panel is IPanel) {
					(panel as IPanel).BackEvent += BackClicked;
				}
				centralbox.PackStart (panel, true, true, 0);
				welcomepanel.Hide ();
			}
		}

		public void AddExportEntry (string name, string shortName, Action<Project, IGUIToolkit> exportAction)
		{
			MenuItem parent = (MenuItem)this.UIManager.GetWidget ("/menubar1/ToolsAction/ExportProjectAction1");
			
			MenuItem item = new MenuItem (name);
			item.Activated += (sender, e) => (exportAction (openedProject, guiToolKit));
			item.Show ();
			(parent.Submenu as Menu).Append (item);
		}

		public IAnalysisWindow SetProject (Project project, ProjectType projectType, CaptureSettings props, EventsFilter filter)
		{
			ExportProjectAction1.Sensitive = true;
			
			this.projectType = projectType;
			openedProject = project;
			if (projectType == ProjectType.FileProject) {
				Title = openedProject.Description.Title +
				" - " + Constants.SOFTWARE_NAME;
			} else {
				Title = Constants.SOFTWARE_NAME;
			}
			MakeActionsSensitive (true, projectType);
			if (projectType == ProjectType.FakeCaptureProject) {
				analysisWindow = new FakeAnalysisComponent ();
			} else {
				analysisWindow = new AnalysisComponent ();
			}
			SetPanel (analysisWindow as Widget);
			analysisWindow.SetProject (project, projectType, props, filter);
			return analysisWindow;
		}

		public void CloseProject ()
		{
			openedProject = null;
			projectType = ProjectType.None;
			(analysisWindow as Gtk.Widget).Destroy ();
			analysisWindow = null;
			ResetGUI ();
		}

		public void SelectProject (List<ProjectDescription> projects)
		{
			OpenProjectPanel panel = new OpenProjectPanel ();
			panel.Projects = projects;
			SetPanel (panel);
		}

		public void CreateNewProject (Project project)
		{
			NewProjectPanel panel = new NewProjectPanel (project);
			panel.Name = "newprojectpanel";
			SetPanel (panel);
		}

		public void CloseAndQuit ()
		{
			Config.EventsBroker.EmitCloseOpenedProject ();
			if (openedProject == null) {
				Config.EventsBroker.EmitQuitApplication ();
			}
		}

		#endregion

		#region Private Methods

		protected override bool OnKeyPressEvent (EventKey evnt)
		{
			bool ret = base.OnKeyPressEvent (evnt);
			if (Focus is Entry) {
				return ret;
			} else {
				Config.EventsBroker.EmitKeyPressed (this, LongoMatch.Core.Common.Keyboard.ParseEvent (evnt));
				return true;
			}
		}

		MenuItem ImportProjectActionMenu {
			get {
				return (MenuItem)this.UIManager.GetWidget ("/menubar1/FileAction/ImportProjectAction");
			}
		}

		private void ConnectSignals ()
		{
			/* Adding Handlers for each event */
			renderingstatebar1.ManageJobs += (e, o) => {
				Config.EventsBroker.EmitManageJobs ();
			};
		}

		private void ConnectMenuSignals ()
		{
			SaveProjectAction.Activated += (o, e) => {
				Config.EventsBroker.EmitSaveProject (openedProject, projectType);
			};
			CloseProjectAction.Activated += (o, e) => {
				Config.EventsBroker.EmitCloseOpenedProject ();
			};
			ExportToProjectFileAction.Activated += (o, e) => {
				Config.EventsBroker.EmitExportProject (openedProject);
			};
			CategoriesTemplatesManagerAction.Activated += (o, e) => {
				Config.EventsBroker.EmitManageCategories ();
			};
			TeamsTemplatesManagerAction.Activated += (o, e) => {
				Config.EventsBroker.EmitManageTeams ();
			};
			ProjectsManagerAction.Activated += (o, e) => {
				Config.EventsBroker.EmitManageProjects ();
			};
			DatabasesManagerAction.Activated += (o, e) => {
				Config.EventsBroker.EmitManageDatabases ();
			};
			PreferencesAction.Activated += (sender, e) => {
				Config.EventsBroker.EmitEditPreferences ();
			};
			ShowProjectStatsAction.Activated += (sender, e) => {
				Config.EventsBroker.EmitShowProjectStats (openedProject);
			}; 
			QuitAction.Activated += (o, e) => {
				CloseAndQuit ();
			};
			openAction.Activated += (sender, e) => {
				Config.EventsBroker.EmitSaveProject (openedProject, projectType);
				Config.EventsBroker.EmitOpenProject ();
			};
			NewPojectAction.Activated += (sender, e) => {
				Config.EventsBroker.EmitNewProject (null);
			};
			ImportProjectAction.Activated += (sender, e) => {
				Config.EventsBroker.EmitImportProject ();
			};
			FullScreenAction.Activated += (object sender, EventArgs e) => {
				Config.EventsBroker.EmitShowFullScreen (FullScreenAction.Active);
			};
		}

		void DestroyPanel (Widget panel)
		{
			if (panel is IPanel) {
				(panel as IPanel).BackEvent -= BackClicked;
			}
			panel.Destroy ();
			panel.Dispose ();
			System.GC.Collect ();
		}

		void RemovePanel (bool stack)
		{
			if (currentPanel == null) {
				return;
			}
			if (stack) {
				stackPanel = currentPanel;
				stackPanel.Visible = false;
			} else {
				DestroyPanel (currentPanel);
				currentPanel = null;
				if (stackPanel != null) {
					DestroyPanel (stackPanel);
					stackPanel = null;
				}
			}
		}

		void BackClicked ()
		{
			if (stackPanel != null) {
				DestroyPanel (currentPanel);
				currentPanel = stackPanel;
				stackPanel.Visible = true;
			} else {
				ResetGUI ();
			}
		}

		private void ResetGUI ()
		{
			Title = Constants.SOFTWARE_NAME;
			MakeActionsSensitive (false, projectType);
			RemovePanel (false);
			welcomepanel.Show ();
		}

		private void MakeActionsSensitive (bool sensitive, ProjectType projectType)
		{
			bool sensitive2 = sensitive && projectType == ProjectType.FileProject;
			CloseProjectAction.Sensitive = sensitive;
			ExportProjectAction1.Sensitive = sensitive;
			ShowProjectStatsAction.Sensitive = sensitive;
			SaveProjectAction.Sensitive = sensitive2;
		}

		protected override bool OnDeleteEvent (Event evnt)
		{
			CloseAndQuit ();
			return true;
		}

		#endregion

		#region Callbacks

		protected void OnVideoConverterToolActionActivated (object sender, System.EventArgs e)
		{
			int res;
			VideoConversionTool converter = new VideoConversionTool ();
			res = converter.Run ();
			converter.Destroy ();
			if (res == (int)ResponseType.Ok) {
				Config.EventsBroker.EmitConvertVideoFiles (converter.Files,
					converter.EncodingSettings);
			}
		}

		protected virtual void OnHelpAction1Activated (object sender, System.EventArgs e)
		{
			try {
				System.Diagnostics.Process.Start (Constants.MANUAL);
			} catch {
			}
		}

		protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
		{
			var about = new LongoMatch.Gui.Dialog.AboutDialog (guiToolKit.Version);
			about.TransientFor = this;
			about.Run ();
			about.Destroy ();
		}

		protected void OnMigrationToolActionActivated (object sender, EventArgs e)
		{
			Config.EventsBroker.EmitMigrateDB ();
		}

		#endregion
	}
}
