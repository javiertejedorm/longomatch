
// This file has been generated by the GUI designer. Do not modify.
namespace LongoMatch.Gui.Dialog
{
	public partial class RenderingJobsDialog
	{
		private global::Gtk.HBox hbox1;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::LongoMatch.Gui.Component.RenderingJobsTreeView renderingjobstreeview2;
		private global::Gtk.VBox vbox2;
		private global::Gtk.Button clearbutton;
		private global::Gtk.Button cancelbutton;
		private global::Gtk.Button retrybutton;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget LongoMatch.Gui.Dialog.RenderingJobsDialog
			this.Name = "LongoMatch.Gui.Dialog.RenderingJobsDialog";
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "longomatch", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			this.Gravity = ((global::Gdk.Gravity)(5));
			this.SkipPagerHint = true;
			this.SkipTaskbarHint = true;
			// Internal child LongoMatch.Gui.Dialog.RenderingJobsDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.renderingjobstreeview2 = new global::LongoMatch.Gui.Component.RenderingJobsTreeView ();
			this.renderingjobstreeview2.CanFocus = true;
			this.renderingjobstreeview2.Name = "renderingjobstreeview2";
			this.GtkScrolledWindow.Add (this.renderingjobstreeview2);
			this.hbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.GtkScrolledWindow]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.clearbutton = new global::Gtk.Button ();
			this.clearbutton.CanFocus = true;
			this.clearbutton.Name = "clearbutton";
			this.clearbutton.UseUnderline = true;
			// Container child clearbutton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w4 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w5 = new global::Gtk.HBox ();
			w5.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w6 = new global::Gtk.Image ();
			w6.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			w5.Add (w6);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w8 = new global::Gtk.Label ();
			w8.LabelProp = global::Mono.Unix.Catalog.GetString ("C_lear finished jobs");
			w8.UseUnderline = true;
			w5.Add (w8);
			w4.Add (w5);
			this.clearbutton.Add (w4);
			this.vbox2.Add (this.clearbutton);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.clearbutton]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.cancelbutton = new global::Gtk.Button ();
			this.cancelbutton.CanFocus = true;
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.UseUnderline = true;
			// Container child cancelbutton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w13 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w14 = new global::Gtk.HBox ();
			w14.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w14.Add (w15);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w17 = new global::Gtk.Label ();
			w17.LabelProp = global::Mono.Unix.Catalog.GetString ("_Cancel job");
			w17.UseUnderline = true;
			w14.Add (w17);
			w13.Add (w14);
			this.cancelbutton.Add (w13);
			this.vbox2.Add (this.cancelbutton);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.cancelbutton]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.retrybutton = new global::Gtk.Button ();
			this.retrybutton.CanFocus = true;
			this.retrybutton.Name = "retrybutton";
			this.retrybutton.UseUnderline = true;
			// Container child retrybutton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w22 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w23 = new global::Gtk.HBox ();
			w23.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w24 = new global::Gtk.Image ();
			w24.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-refresh", global::Gtk.IconSize.Menu);
			w23.Add (w24);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w26 = new global::Gtk.Label ();
			w26.LabelProp = global::Mono.Unix.Catalog.GetString ("Retry job");
			w26.UseUnderline = true;
			w23.Add (w26);
			w22.Add (w23);
			this.retrybutton.Add (w22);
			this.vbox2.Add (this.retrybutton);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.retrybutton]));
			w30.Position = 2;
			w30.Expand = false;
			w30.Fill = false;
			this.hbox1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox2]));
			w31.Position = 1;
			w31.Expand = false;
			w31.Fill = false;
			w1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox1]));
			w32.Position = 0;
			// Internal child LongoMatch.Gui.Dialog.RenderingJobsDialog.ActionArea
			global::Gtk.HButtonBox w33 = this.ActionArea;
			w33.Name = "dialog1_ActionArea";
			w33.Spacing = 10;
			w33.BorderWidth = ((uint)(5));
			w33.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w34 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w33 [this.buttonOk]));
			w34.Expand = false;
			w34.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 212;
			this.cancelbutton.Hide ();
			this.retrybutton.Hide ();
			this.Show ();
		}
	}
}
