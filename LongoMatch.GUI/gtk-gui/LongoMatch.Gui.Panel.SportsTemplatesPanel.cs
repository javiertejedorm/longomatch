
// This file has been generated by the GUI designer. Do not modify.
namespace LongoMatch.Gui.Panel
{
	public partial class SportsTemplatesPanel
	{
		private global::Gtk.VBox vbox3;
		private global::Gtk.HBox hbox1;
		private global::Gtk.VBox teamsvbox;
		private global::Gtk.Frame frame4;
		private global::Gtk.Alignment GtkAlignment7;
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		private global::Gtk.TreeView templatestreeview;
		private global::Gtk.Label GtkLabel7;
		private global::Gtk.HButtonBox hbuttonbox4;
		private global::Gtk.Button newteam;
		private global::Gtk.Button deleteteambutton;
		private global::Gtk.VSeparator vseparator2;
		private global::LongoMatch.Gui.Component.AnalysisTemplateEditor analysistemplateeditor;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.HButtonBox hbuttonbox3;
		private global::Gtk.Button backbutton;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget LongoMatch.Gui.Panel.SportsTemplatesPanel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "LongoMatch.Gui.Panel.SportsTemplatesPanel";
			// Container child LongoMatch.Gui.Panel.SportsTemplatesPanel.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.teamsvbox = new global::Gtk.VBox ();
			this.teamsvbox.WidthRequest = 280;
			this.teamsvbox.Name = "teamsvbox";
			this.teamsvbox.Spacing = 6;
			// Container child teamsvbox.Gtk.Box+BoxChild
			this.frame4 = new global::Gtk.Frame ();
			this.frame4.Name = "frame4";
			this.frame4.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame4.Gtk.Container+ContainerChild
			this.GtkAlignment7 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment7.Name = "GtkAlignment7";
			this.GtkAlignment7.LeftPadding = ((uint)(12));
			// Container child GtkAlignment7.Gtk.Container+ContainerChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.templatestreeview = new global::Gtk.TreeView ();
			this.templatestreeview.CanFocus = true;
			this.templatestreeview.Name = "templatestreeview";
			this.GtkScrolledWindow1.Add (this.templatestreeview);
			this.GtkAlignment7.Add (this.GtkScrolledWindow1);
			this.frame4.Add (this.GtkAlignment7);
			this.GtkLabel7 = new global::Gtk.Label ();
			this.GtkLabel7.Name = "GtkLabel7";
			this.GtkLabel7.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Analysis templates</b>");
			this.GtkLabel7.UseMarkup = true;
			this.frame4.LabelWidget = this.GtkLabel7;
			this.teamsvbox.Add (this.frame4);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.teamsvbox [this.frame4]));
			w4.Position = 0;
			// Container child teamsvbox.Gtk.Box+BoxChild
			this.hbuttonbox4 = new global::Gtk.HButtonBox ();
			this.hbuttonbox4.Name = "hbuttonbox4";
			// Container child hbuttonbox4.Gtk.ButtonBox+ButtonBoxChild
			this.newteam = new global::Gtk.Button ();
			this.newteam.CanFocus = true;
			this.newteam.Name = "newteam";
			this.newteam.UseUnderline = true;
			// Container child newteam.Gtk.Container+ContainerChild
			global::Gtk.Alignment w5 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w6 = new global::Gtk.HBox ();
			w6.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w7 = new global::Gtk.Image ();
			w7.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Dialog);
			w6.Add (w7);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w9 = new global::Gtk.Label ();
			w9.LabelProp = global::Mono.Unix.Catalog.GetString ("New");
			w9.UseUnderline = true;
			w6.Add (w9);
			w5.Add (w6);
			this.newteam.Add (w5);
			this.hbuttonbox4.Add (this.newteam);
			global::Gtk.ButtonBox.ButtonBoxChild w13 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox4 [this.newteam]));
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbuttonbox4.Gtk.ButtonBox+ButtonBoxChild
			this.deleteteambutton = new global::Gtk.Button ();
			this.deleteteambutton.CanFocus = true;
			this.deleteteambutton.Name = "deleteteambutton";
			this.deleteteambutton.UseUnderline = true;
			// Container child deleteteambutton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w14 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w15 = new global::Gtk.HBox ();
			w15.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w16 = new global::Gtk.Image ();
			w16.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-remove", global::Gtk.IconSize.Dialog);
			w15.Add (w16);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w18 = new global::Gtk.Label ();
			w18.LabelProp = global::Mono.Unix.Catalog.GetString ("Delete");
			w18.UseUnderline = true;
			w15.Add (w18);
			w14.Add (w15);
			this.deleteteambutton.Add (w14);
			this.hbuttonbox4.Add (this.deleteteambutton);
			global::Gtk.ButtonBox.ButtonBoxChild w22 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox4 [this.deleteteambutton]));
			w22.Position = 1;
			w22.Expand = false;
			w22.Fill = false;
			this.teamsvbox.Add (this.hbuttonbox4);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.teamsvbox [this.hbuttonbox4]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			this.hbox1.Add (this.teamsvbox);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.teamsvbox]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vseparator2 = new global::Gtk.VSeparator ();
			this.vseparator2.Name = "vseparator2";
			this.hbox1.Add (this.vseparator2);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vseparator2]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.analysistemplateeditor = new global::LongoMatch.Gui.Component.AnalysisTemplateEditor ();
			this.analysistemplateeditor.Events = ((global::Gdk.EventMask)(256));
			this.analysistemplateeditor.Name = "analysistemplateeditor";
			this.analysistemplateeditor.Edited = false;
			this.hbox1.Add (this.analysistemplateeditor);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.analysistemplateeditor]));
			w26.Position = 2;
			this.vbox3.Add (this.hbox1);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox1]));
			w27.Position = 0;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox3.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hseparator1]));
			w28.Position = 1;
			w28.Expand = false;
			w28.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbuttonbox3 = new global::Gtk.HButtonBox ();
			this.hbuttonbox3.Name = "hbuttonbox3";
			this.hbuttonbox3.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(3));
			// Container child hbuttonbox3.Gtk.ButtonBox+ButtonBoxChild
			this.backbutton = new global::Gtk.Button ();
			this.backbutton.CanFocus = true;
			this.backbutton.Name = "backbutton";
			this.backbutton.UseUnderline = true;
			// Container child backbutton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w29 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w30 = new global::Gtk.HBox ();
			w30.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w31 = new global::Gtk.Image ();
			w31.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-back", global::Gtk.IconSize.Dialog);
			w30.Add (w31);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w33 = new global::Gtk.Label ();
			w33.LabelProp = global::Mono.Unix.Catalog.GetString ("_Back");
			w33.UseUnderline = true;
			w30.Add (w33);
			w29.Add (w30);
			this.backbutton.Add (w29);
			this.hbuttonbox3.Add (this.backbutton);
			global::Gtk.ButtonBox.ButtonBoxChild w37 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox3 [this.backbutton]));
			w37.Expand = false;
			w37.Fill = false;
			this.vbox3.Add (this.hbuttonbox3);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbuttonbox3]));
			w38.Position = 2;
			w38.Expand = false;
			w38.Fill = false;
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}