// CapturerBin.cs
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
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301, USA.
//
//

using System;
using Gtk;
using Gdk;
using GLib;
using LongoMatch.Video;
using LongoMatch.Video.Capturer;
using LongoMatch.Video.Utils;
using Mono.Unix;

namespace LongoMatch.Gui
{
	
	
	[System.ComponentModel.Category("CesarPlayer")]
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CapturerBin : Gtk.Bin
	{
		public event EventHandler CaptureFinished;
		
		private Pixbuf logopix;
		private uint outputWidth;
		private uint outputHeight;
		private uint videoBitrate;
		private uint audioBitrate;
		private GccVideoEncoderType  videoEncoder;
		private GccAudioEncoderType audioEncoder;
		private GccVideoMuxerType videoMuxer;
		private string outputFile;
		private const int THUMBNAIL_MAX_WIDTH = 100;		
		
		ICapturer capturer;
		
		public CapturerBin()
		{
			this.Build();
				
			outputWidth = 320;
			outputHeight = 240;
			videoBitrate = 1000;
			audioBitrate = 128;
			videoEncoder = GccVideoEncoderType.H264;
			audioEncoder = GccAudioEncoderType.Aac;
			videoMuxer = GccVideoMuxerType.Mp4;
			outputFile = "";
			Type = CapturerType.FAKE;
		}		
		
		public CapturerType Type {
			set{
				if (capturer != null){
					capturer.Stop();
					capturerhbox.Remove(capturer as Gtk.Widget);
				}
				MultimediaFactory factory = new MultimediaFactory();
				capturer = factory.getCapturer(value);	
				capturer.EllapsedTime += OnTick;
				if (value != CapturerType.FAKE){
					capturerhbox.Add((Widget)capturer);
					(capturer as Widget).Visible = true;
					capturerhbox.Visible = true;
					logodrawingarea.Visible = false;
				}
				else{
					logodrawingarea.Visible = true;
					capturerhbox.Visible = false;
				}
				SetProperties();
			}
		}
		
		public string Logo{
			set{
				try{
					this.logopix = new Pixbuf(value);
				}catch{
					/* FIXME: Add log */
				}
			}
		}
		 
		public string OutputFile {
			set{
				capturer.OutputFile= value;
				outputFile = value;
			}			
		}		
				
		public uint VideoBitrate {
			set{
				capturer.VideoBitrate=value;
				videoBitrate = value;
			}			
		}
		
		public uint AudioBitrate {
			set{
				capturer.AudioBitrate=value;
				audioBitrate = value;
			}
		}
		
		public uint OutputWidth {
			set {
				capturer.OutputWidth = value;
				outputWidth = value;
			}
		} 
		
		public uint OutputHeight {
			set {
				capturer.OutputHeight = value;
				outputHeight = value;
			}
		} 
		
		public int CurrentTime {
			get {
				return capturer.CurrentTime;
			}
		}
		
		public CapturePropertiesStruct CaptureProperties{
			set{
				outputWidth = value.Width;
				outputHeight = value.Height;
				audioBitrate = value.AudioBitrate;
				videoBitrate = value.VideoBitrate;
				audioEncoder = value.AudioEncoder;
				videoEncoder = value.VideoEncoder;
				videoMuxer = value.Muxer;
			}
		}
		
		public void TogglePause(){
			capturer.TogglePause();
		}
		
		public void Start(){
			capturer.Start();
		}
		
		public void Stop(){
			capturer.Stop();
		}
		
		public void Run(){
			capturer.Run();
		}

		public void Close(){
			capturer.Close();
		}
		
		public Pixbuf CurrentMiniatureFrame {
			get {
				int h, w;
				double rate;
				Pixbuf scaled_pix;
				Pixbuf pix = capturer.CurrentFrame;
				
				if (pix == null)
					return null;
				
				w = pix.Width;
				h = pix.Height;
				rate = (double)w / (double)h;
				
				if (h > w) {
					w = (int)(THUMBNAIL_MAX_WIDTH * rate);
					h = THUMBNAIL_MAX_WIDTH;
				} else {
					h = (int)(THUMBNAIL_MAX_WIDTH / rate);
					w = THUMBNAIL_MAX_WIDTH;
				}
				scaled_pix = pix.ScaleSimple (w, h, Gdk.InterpType.Bilinear);
				pix.Dispose();
					
				return scaled_pix;				                       
			}
		}
		
		public void SetVideoEncoder(GccVideoEncoderType type){
			capturer.SetVideoEncoder(type);
			videoEncoder = type;
		}
		
		public void SetAudioEncoder(GccAudioEncoderType type){
			capturer.SetAudioEncoder(type);
			audioEncoder = type;
		}
		
		public void SetVideoMuxer(GccVideoMuxerType type){
			capturer.SetVideoMuxer(type);
			videoMuxer = type;
		}
		
		private void SetProperties(){
			capturer.OutputFile = outputFile;
			capturer.OutputHeight = outputHeight;
			capturer.OutputWidth = outputWidth;
			capturer.SetVideoEncoder(videoEncoder);
			capturer.SetAudioEncoder(audioEncoder);
			capturer.SetVideoMuxer(videoMuxer);	
			capturer.VideoBitrate = videoBitrate;
			capturer.AudioBitrate = audioBitrate;
		}

		protected virtual void OnRecbuttonClicked (object sender, System.EventArgs e)
		{
			Start();
			recbutton.Visible = false;
			pausebutton.Visible = true;
			stopbutton.Visible = true;
		}

		protected virtual void OnPausebuttonClicked (object sender, System.EventArgs e)
		{
			TogglePause();
			recbutton.Visible = true;
			pausebutton.Visible = false;			
		}

		protected virtual void OnStopbuttonClicked (object sender, System.EventArgs e)
		{
			MessageDialog md = new MessageDialog((Gtk.Window)this.Toplevel, DialogFlags.Modal, MessageType.Question, ButtonsType.YesNo,
			                                     Catalog.GetString("You are going to stop and finish the current capture."+"\n"+
			                                                       "Do you want to proceed?"));
			if (md.Run() == (int)ResponseType.Yes){
				Stop();
				recbutton.Visible = true;
				pausebutton.Visible = false;
				stopbutton.Visible = false;
				if (CaptureFinished != null)
					CaptureFinished(this, new EventArgs());
			}
			md.Destroy();
		}				
		
		protected virtual void OnTick (int ellapsedTime){
			timelabel.Text = "Time: " + TimeString.MSecondsToSecondsString(CurrentTime);
		}
		
		protected virtual void OnLogodrawingareaExposeEvent (object o, Gtk.ExposeEventArgs args)
		{	
			Gdk.Window win;
			Rectangle area;
			Pixbuf frame;
			Pixbuf drawing;
			int width, height, allocWidth, allocHeight, logoX, logoY;
			float ratio;
			
			if (logopix == null)
				return;

			win = logodrawingarea.GdkWindow;
			width = logopix.Width;
			height = logopix.Height;
			allocWidth = logodrawingarea.Allocation.Width;
			allocHeight = logodrawingarea.Allocation.Height;
			area = args.Event.Area;
			
			/* Checking if allocated space is smaller than our logo */
			if ((float) allocWidth / width > (float) allocHeight / height) {
				ratio = (float) allocHeight / height;
			} else {
				ratio = (float) allocWidth / width;
			}
			width = (int) (width * ratio);
			height = (int) (height * ratio);
			
			logoX = (allocWidth / 2) - (width / 2);
			logoY = (allocHeight / 2) - (height / 2);

			/* Drawing our frame */
			frame = new Pixbuf(Colorspace.Rgb, false, 8, allocWidth, allocHeight);
			logopix.Composite(frame, 0, 0, allocWidth, allocHeight, logoX, logoY, 
			                  ratio, ratio, InterpType.Bilinear, 255);
			
			win.DrawPixbuf (this.Style.BlackGC, frame, 0, 0,
			                0, 0, allocWidth, allocHeight,
			                RgbDither.Normal, 0, 0);
			frame.Dispose();
			return;
		}
	}
}
