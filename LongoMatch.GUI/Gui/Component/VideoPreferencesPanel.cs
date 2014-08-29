//
//  Copyright (C) 2013 Andoni Morales Alastruey
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
using Gtk;
using LongoMatch.Common;
using Misc = LongoMatch.Gui.Helpers.Misc;

namespace LongoMatch.Gui.Component
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class VideoPreferencesPanel : Gtk.Bin
	{
		CheckButton overlayTitle, enableSound;
		
		public VideoPreferencesPanel ()
		{
			this.Build ();
			
			if (Config.FPS_N == 30000) {
				fpscombobox.Active = 1;
			} else {
				fpscombobox.Active = 0;
			}
			fpscombobox.Changed += HandleFPSChanged;
			Misc.FillImageFormat (renderimagecombo, Config.RenderVideoStandard);
			Misc.FillEncodingFormat (renderenccombo, Config.RenderEncodingProfile);
			Misc.FillQuality (renderqualcombo, Config.RenderEncodingQuality);
			
			Misc.FillImageFormat (captureimagecombo, Config.CaptureVideoStandard);
			Misc.FillEncodingFormat (captureenccombo, Config.CaptureEncodingProfile);
			Misc.FillQuality (capturequalcombo, Config.CaptureEncodingQuality);
			
			renderimagecombo.Changed += HandleImageChanged;
			captureimagecombo.Changed += HandleImageChanged;
			
			renderenccombo.Changed += HandleEncodingChanged;  
			captureenccombo.Changed += HandleEncodingChanged;  
			
			renderqualcombo.Changed += HandleQualityChanged;
			captureimagecombo.Changed += HandleImageChanged;
			
			enableSound = new CheckButton ();
			rendertable.Attach (enableSound, 1, 2, 3, 4,
			                    AttachOptions.Fill,
			                    AttachOptions.Fill, 0, 0);
			enableSound.CanFocus = false;
			enableSound.Show ();
			enableSound.Active = Config.EnableAudio;
			enableSound.Toggled += (sender, e) => {
				Config.EnableAudio = enableSound.Active;};

			overlayTitle = new CheckButton ();
			rendertable.Attach (overlayTitle, 1, 2, 4, 5,
			                    AttachOptions.Fill,
			                    AttachOptions.Fill, 0, 0);
			overlayTitle.CanFocus = false;
			overlayTitle.Show ();
			overlayTitle.Active = Config.OverlayTitle;
			overlayTitle.Toggled += (sender, e) => {
				Config.OverlayTitle = overlayTitle.Active;};
			
			SizeGroup sgroup = new SizeGroup (SizeGroupMode.Horizontal);
			foreach (Widget w in capturetable) {
				if (w is Label) {
					sgroup.AddWidget (w);
				}
			}
			foreach (Widget w in rendertable) {
				if (w is Label) {
					sgroup.AddWidget (w);
				}
			}
			sgroup.AddWidget (fameratelabel);
		}

		void HandleFPSChanged (object sender, EventArgs e)
		{
			if (fpscombobox.ActiveText == "25 fps") {
				Config.FPS_N = 25;
				Config.FPS_D = 1;
			} else {
				Config.FPS_N = 30;
				Config.FPS_D = 1;
			}
		}

		void HandleQualityChanged (object sender, EventArgs e)
		{
			EncodingQuality qual;
			ListStore store;
			TreeIter iter;
			ComboBox combo = sender as ComboBox;
			
			combo.GetActiveIter (out iter);
			store = combo.Model as ListStore;
			qual = (EncodingQuality) store.GetValue(iter, 1);
			
			if (combo == renderqualcombo)
				Config.RenderEncodingQuality = qual;
			else
				Config.CaptureEncodingQuality = qual;
		}

		void HandleImageChanged (object sender, EventArgs e)
		{
			VideoStandard std;
			ListStore store;
			TreeIter iter;
			ComboBox combo = sender as ComboBox;
			
			combo.GetActiveIter (out iter);
			store = combo.Model as ListStore;
			std = (VideoStandard) store.GetValue(iter, 1);
			
			if (combo == renderimagecombo)
				Config.RenderVideoStandard = std;
			else
				Config.CaptureVideoStandard = std;
			
		}

		void HandleEncodingChanged (object sender, EventArgs e)
		{
			EncodingProfile enc;
			ListStore store;
			TreeIter iter;
			ComboBox combo = sender as ComboBox;
			
			combo.GetActiveIter (out iter);
			store = combo.Model as ListStore;
			enc = (EncodingProfile) store.GetValue(iter, 1);
			
			if (combo == renderenccombo)
				Config.RenderEncodingProfile = enc;
			else
				Config.CaptureEncodingProfile = enc;
			
		}
	}
}

