// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace LongoMatch.Gui.Component {
    
    
    public partial class TimeNodeProperties {
        
        private Gtk.VBox vbox3;
        
        private Gtk.HBox hbox4;
        
        private Gtk.Label label1;
        
        private Gtk.Entry nameentry;
        
        private LongoMatch.Gui.Component.TimeAdjustWidget timeadjustwidget1;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Label label4;
        
        private Gtk.ColorButton colorbutton1;
        
        private Gtk.Button changebuton;
        
        private Gtk.Label hotKeyLabel;
        
        private Gtk.Label label6;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget LongoMatch.Gui.Component.TimeNodeProperties
            Stetic.BinContainer.Attach(this);
            this.Name = "LongoMatch.Gui.Component.TimeNodeProperties";
            // Container child LongoMatch.Gui.Component.TimeNodeProperties.Gtk.Container+ContainerChild
            this.vbox3 = new Gtk.VBox();
            this.vbox3.Name = "vbox3";
            this.vbox3.Spacing = 6;
            // Container child vbox3.Gtk.Box+BoxChild
            this.hbox4 = new Gtk.HBox();
            this.hbox4.Name = "hbox4";
            this.hbox4.Spacing = 6;
            // Container child hbox4.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Name:");
            this.hbox4.Add(this.label1);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.hbox4[this.label1]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            // Container child hbox4.Gtk.Box+BoxChild
            this.nameentry = new Gtk.Entry();
            this.nameentry.CanFocus = true;
            this.nameentry.Name = "nameentry";
            this.nameentry.IsEditable = true;
            this.nameentry.InvisibleChar = '●';
            this.hbox4.Add(this.nameentry);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox4[this.nameentry]));
            w2.Position = 1;
            this.vbox3.Add(this.hbox4);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox3[this.hbox4]));
            w3.Position = 0;
            w3.Fill = false;
            // Container child vbox3.Gtk.Box+BoxChild
            this.timeadjustwidget1 = new LongoMatch.Gui.Component.TimeAdjustWidget();
            this.timeadjustwidget1.Events = ((Gdk.EventMask)(256));
            this.timeadjustwidget1.Name = "timeadjustwidget1";
            this.vbox3.Add(this.timeadjustwidget1);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox3[this.timeadjustwidget1]));
            w4.Position = 1;
            w4.Fill = false;
            // Container child vbox3.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("Color:        ");
            this.hbox2.Add(this.label4);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox2[this.label4]));
            w5.Position = 0;
            w5.Expand = false;
            w5.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.colorbutton1 = new Gtk.ColorButton();
            this.colorbutton1.CanFocus = true;
            this.colorbutton1.Events = ((Gdk.EventMask)(784));
            this.colorbutton1.Name = "colorbutton1";
            this.hbox2.Add(this.colorbutton1);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox2[this.colorbutton1]));
            w6.Position = 1;
            w6.Expand = false;
            w6.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.changebuton = new Gtk.Button();
            this.changebuton.CanFocus = true;
            this.changebuton.Name = "changebuton";
            this.changebuton.UseUnderline = true;
            this.changebuton.Label = Mono.Unix.Catalog.GetString("Change");
            this.hbox2.Add(this.changebuton);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.hbox2[this.changebuton]));
            w7.PackType = ((Gtk.PackType)(1));
            w7.Position = 2;
            w7.Expand = false;
            w7.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.hotKeyLabel = new Gtk.Label();
            this.hotKeyLabel.Name = "hotKeyLabel";
            this.hotKeyLabel.LabelProp = Mono.Unix.Catalog.GetString("none");
            this.hbox2.Add(this.hotKeyLabel);
            Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.hbox2[this.hotKeyLabel]));
            w8.PackType = ((Gtk.PackType)(1));
            w8.Position = 3;
            w8.Expand = false;
            w8.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.label6 = new Gtk.Label();
            this.label6.Name = "label6";
            this.label6.LabelProp = Mono.Unix.Catalog.GetString("HotKey:");
            this.hbox2.Add(this.label6);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.hbox2[this.label6]));
            w9.PackType = ((Gtk.PackType)(1));
            w9.Position = 4;
            w9.Expand = false;
            w9.Fill = false;
            this.vbox3.Add(this.hbox2);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.vbox3[this.hbox2]));
            w10.Position = 2;
            w10.Fill = false;
            this.Add(this.vbox3);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
            this.nameentry.Changed += new System.EventHandler(this.OnNameentryChanged);
            this.timeadjustwidget1.LeadTimeChanged += new System.EventHandler(this.OnTimeadjustwidget1LeadTimeChanged);
            this.timeadjustwidget1.LagTimeChanged += new System.EventHandler(this.OnTimeadjustwidget1LagTimeChanged);
            this.colorbutton1.ColorSet += new System.EventHandler(this.OnColorbutton1ColorSet);
            this.changebuton.Clicked += new System.EventHandler(this.OnChangebutonClicked);
        }
    }
}
