
// This file has been generated by the GUI designer. Do not modify.
namespace LongoMatch.Gui.Component
{
	public partial class CodingWidget
	{
		private global::Gtk.Notebook notebook;
		private global::Gtk.HPaned dashboardhpaned;
		private global::Gtk.DrawingArea teamsdrawingarea;
		private global::Gtk.HBox hbox5;
		private global::LongoMatch.Gui.Component.DashboardWidget buttonswidget;
		private global::Gtk.Label label2;
		private global::LongoMatch.Gui.Component.Timeline timeline;
		private global::Gtk.Label label3;
		private global::LongoMatch.Gui.Component.PlaysPositionViewer playspositionviewer1;
		private global::Gtk.Label label5;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget LongoMatch.Gui.Component.CodingWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "LongoMatch.Gui.Component.CodingWidget";
			// Container child LongoMatch.Gui.Component.CodingWidget.Gtk.Container+ContainerChild
			this.notebook = new global::Gtk.Notebook ();
			this.notebook.CanFocus = true;
			this.notebook.Name = "notebook";
			this.notebook.CurrentPage = 0;
			this.notebook.TabPos = ((global::Gtk.PositionType)(0));
			this.notebook.ShowBorder = false;
			// Container child notebook.Gtk.Notebook+NotebookChild
			this.dashboardhpaned = new global::Gtk.HPaned ();
			this.dashboardhpaned.CanFocus = true;
			this.dashboardhpaned.Name = "dashboardhpaned";
			this.dashboardhpaned.Position = 276;
			// Container child dashboardhpaned.Gtk.Paned+PanedChild
			this.teamsdrawingarea = new global::Gtk.DrawingArea ();
			this.teamsdrawingarea.Name = "teamsdrawingarea";
			this.dashboardhpaned.Add (this.teamsdrawingarea);
			global::Gtk.Paned.PanedChild w1 = ((global::Gtk.Paned.PanedChild)(this.dashboardhpaned [this.teamsdrawingarea]));
			w1.Resize = false;
			// Container child dashboardhpaned.Gtk.Paned+PanedChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonswidget = new global::LongoMatch.Gui.Component.DashboardWidget ();
			this.buttonswidget.Events = ((global::Gdk.EventMask)(256));
			this.buttonswidget.Name = "buttonswidget";
			this.buttonswidget.Edited = false;
			this.hbox5.Add (this.buttonswidget);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.buttonswidget]));
			w2.Position = 0;
			this.dashboardhpaned.Add (this.hbox5);
			this.notebook.Add (this.dashboardhpaned);
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("page1");
			this.notebook.SetTabLabel (this.dashboardhpaned, this.label2);
			this.label2.ShowAll ();
			// Container child notebook.Gtk.Notebook+NotebookChild
			this.timeline = new global::LongoMatch.Gui.Component.Timeline ();
			this.timeline.Events = ((global::Gdk.EventMask)(256));
			this.timeline.Name = "timeline";
			this.notebook.Add (this.timeline);
			global::Gtk.Notebook.NotebookChild w5 = ((global::Gtk.Notebook.NotebookChild)(this.notebook [this.timeline]));
			w5.Position = 1;
			// Notebook tab
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("page2");
			this.notebook.SetTabLabel (this.timeline, this.label3);
			this.label3.ShowAll ();
			// Container child notebook.Gtk.Notebook+NotebookChild
			this.playspositionviewer1 = new global::LongoMatch.Gui.Component.PlaysPositionViewer ();
			this.playspositionviewer1.Events = ((global::Gdk.EventMask)(256));
			this.playspositionviewer1.Name = "playspositionviewer1";
			this.notebook.Add (this.playspositionviewer1);
			global::Gtk.Notebook.NotebookChild w6 = ((global::Gtk.Notebook.NotebookChild)(this.notebook [this.playspositionviewer1]));
			w6.Position = 2;
			// Notebook tab
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("page3");
			this.notebook.SetTabLabel (this.playspositionviewer1, this.label5);
			this.label5.ShowAll ();
			this.Add (this.notebook);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
