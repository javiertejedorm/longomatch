// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace longomatch {
    
    
    public partial class CapturerBin {
        
        private Gtk.VBox vbox1;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Button recbutton;
        
        private Gtk.Button pausebutton;
        
        private Gtk.Button stopbutton;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget longomatch.CapturerBin
            Stetic.BinContainer.Attach(this);
            this.Name = "longomatch.CapturerBin";
            // Container child longomatch.CapturerBin.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.recbutton = new Gtk.Button();
            this.recbutton.CanFocus = true;
            this.recbutton.Name = "recbutton";
            this.recbutton.UseUnderline = true;
            this.recbutton.Relief = ((Gtk.ReliefStyle)(2));
            // Container child recbutton.Gtk.Container+ContainerChild
            Gtk.Alignment w1 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w2 = new Gtk.HBox();
            w2.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w3 = new Gtk.Image();
            w3.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-play", Gtk.IconSize.Menu, 16);
            w2.Add(w3);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w5 = new Gtk.Label();
            w5.LabelProp = "";
            w2.Add(w5);
            w1.Add(w2);
            this.recbutton.Add(w1);
            this.hbox1.Add(this.recbutton);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.hbox1[this.recbutton]));
            w9.Position = 0;
            w9.Expand = false;
            w9.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.pausebutton = new Gtk.Button();
            this.pausebutton.CanFocus = true;
            this.pausebutton.Name = "pausebutton";
            this.pausebutton.UseUnderline = true;
            this.pausebutton.Relief = ((Gtk.ReliefStyle)(2));
            // Container child pausebutton.Gtk.Container+ContainerChild
            Gtk.Alignment w10 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w11 = new Gtk.HBox();
            w11.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w12 = new Gtk.Image();
            w12.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-pause", Gtk.IconSize.Button, 20);
            w11.Add(w12);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w14 = new Gtk.Label();
            w14.LabelProp = "";
            w11.Add(w14);
            w10.Add(w11);
            this.pausebutton.Add(w10);
            this.hbox1.Add(this.pausebutton);
            Gtk.Box.BoxChild w18 = ((Gtk.Box.BoxChild)(this.hbox1[this.pausebutton]));
            w18.Position = 1;
            w18.Expand = false;
            w18.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.stopbutton = new Gtk.Button();
            this.stopbutton.CanFocus = true;
            this.stopbutton.Name = "stopbutton";
            this.stopbutton.UseUnderline = true;
            this.stopbutton.Relief = ((Gtk.ReliefStyle)(2));
            // Container child stopbutton.Gtk.Container+ContainerChild
            Gtk.Alignment w19 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w20 = new Gtk.HBox();
            w20.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w21 = new Gtk.Image();
            w21.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-stop", Gtk.IconSize.Button, 20);
            w20.Add(w21);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w23 = new Gtk.Label();
            w23.LabelProp = "";
            w20.Add(w23);
            w19.Add(w20);
            this.stopbutton.Add(w19);
            this.hbox1.Add(this.stopbutton);
            Gtk.Box.BoxChild w27 = ((Gtk.Box.BoxChild)(this.hbox1[this.stopbutton]));
            w27.Position = 2;
            w27.Expand = false;
            w27.Fill = false;
            this.vbox1.Add(this.hbox1);
            Gtk.Box.BoxChild w28 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
            w28.Position = 1;
            w28.Expand = false;
            w28.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
        }
    }
}
