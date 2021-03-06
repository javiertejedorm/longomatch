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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Unix;
using GLib;

using LongoMatch.Core.Common;
using LongoMatch.Core.Interfaces.Multimedia;
using LongoMatch.Video.Common;
using LongoMatch.Core.Store;
using LongoMatch.Core.Handlers;

namespace LongoMatch.Video.Capturer
{

	#region Autogenerated code
	public  class GstCameraCapturer : GLib.Object, ICapturer
	{

		public event EllpasedTimeHandler EllapsedTime;
		public event ErrorHandler Error;
		public event DeviceChangeHandler DeviceChange;
		public event MediaInfoHandler MediaInfo;

		private LiveSourceTimer timer;

		[DllImport ("libcesarplayer.dll")]
		static extern unsafe IntPtr gst_camera_capturer_new (out IntPtr err);

		[DllImport ("libcesarplayer.dll")]
		static extern unsafe IntPtr gst_camera_capturer_configure (
			IntPtr raw, IntPtr output_file, int type, IntPtr source_element,
			IntPtr device_id, int width, int height, int fps_n, int fps_d,
			int video_encoder, int audio_encoder, int muxer,
			uint video_bitrate, uint audio_bitrate, bool record_audio,
			uint output_width, uint output_height, IntPtr window_handle,
			out IntPtr err);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_stop (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_toggle_pause (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_start (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_run (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_close (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern void gst_camera_capturer_expose (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern IntPtr gst_camera_capturer_get_type ();

		[DllImport ("libcesarplayer.dll")]
		static extern IntPtr gst_camera_capturer_enum_audio_devices ();

		[DllImport ("libcesarplayer.dll")]
		static extern IntPtr gst_camera_capturer_enum_video_devices (string devname);

		[DllImport ("libcesarplayer.dll")]
		static extern IntPtr gst_camera_capturer_get_current_frame (IntPtr raw);

		[DllImport ("libcesarplayer.dll")]
		static extern IntPtr gst_camera_capturer_unref_pixbuf (IntPtr raw);

		public unsafe GstCameraCapturer (string filename) : base (IntPtr.Zero)
		{
			if (GetType () != typeof(GstCameraCapturer)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			IntPtr error = IntPtr.Zero;
			Raw = gst_camera_capturer_new (out error);
			if (error != IntPtr.Zero)
				throw new GLib.GException (error);

			timer = new LiveSourceTimer ();
			timer.EllapsedTime += delegate(Time ellapsedTime) {
				if (EllapsedTime != null)
					EllapsedTime (ellapsedTime);
			};
			
			this.GlibError += (o, args) => {
				if (Error != null) {
					Error (this, args.Message);
				}
			};
			
			this.GlibDeviceChange += (o, args) => {
				if (DeviceChange != null) {
					DeviceChange (args.DeviceChange);
				}
			};
			
			this.GlibMediaInfo += (o, args) => {
				if (MediaInfo != null) {
					MediaInfo (args.Width, args.Height, args.ParN, args.ParD);
				}
			};
		}

		#pragma warning disable 0169
		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ErrorSignalDelegate (IntPtr arg0,IntPtr arg1,IntPtr gch);

		static void ErrorSignalCallback (IntPtr arg0, IntPtr arg1, IntPtr gch)
		{
			ErrorArgs args = new ErrorArgs ();
			try {
				GLib.Signal sig = ((GCHandle)gch).Target as GLib.Signal;
				if (sig == null)
					throw new Exception ("Unknown signal GC handle received " + gch);

				args.Args = new object[1];
				args.Args [0] = GLib.Marshaller.Utf8PtrToString (arg1);
				GlibErrorHandler handler = (GlibErrorHandler)sig.Handler;
				handler (GLib.Object.GetObject (arg0), args);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ErrorVMDelegate (IntPtr gcc,IntPtr message);

		static ErrorVMDelegate ErrorVMCallback;

		static void error_cb (IntPtr gcc, IntPtr message)
		{
			try {
				GstCameraCapturer gcc_managed = GLib.Object.GetObject (gcc, false) as GstCameraCapturer;
				gcc_managed.OnError (GLib.Marshaller.Utf8PtrToString (message));
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		private static void OverrideError (GLib.GType gtype)
		{
			if (ErrorVMCallback == null)
				ErrorVMCallback = new ErrorVMDelegate (error_cb);
			OverrideVirtualMethod (gtype, "error", ErrorVMCallback);
		}

		[GLib.DefaultSignalHandler (Type = typeof(LongoMatch.Video.Capturer.GstCameraCapturer), ConnectionMethod = "OverrideError")]
		protected virtual void OnError (string message)
		{
			GLib.Value ret = GLib.Value.Empty;
			GLib.ValueArray inst_and_params = new GLib.ValueArray (2);
			GLib.Value[] vals = new GLib.Value [2];
			vals [0] = new GLib.Value (this);
			inst_and_params.Append (vals [0]);
			vals [1] = new GLib.Value (message);
			inst_and_params.Append (vals [1]);
			g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
			foreach (GLib.Value v in vals)
				v.Dispose ();
		}

		[GLib.Signal ("error")]
		public event GlibErrorHandler GlibError {
			add {
				GLib.Signal sig = GLib.Signal.Lookup (this, "error", new ErrorSignalDelegate (ErrorSignalCallback));
				sig.AddDelegate (value);
			}
			remove {
				GLib.Signal sig = GLib.Signal.Lookup (this, "error", new ErrorSignalDelegate (ErrorSignalCallback));
				sig.RemoveDelegate (value);
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DeviceChangeSignalDelegate (IntPtr arg0,int arg1,IntPtr gch);

		static void DeviceChangeSignalCallback (IntPtr arg0, int arg1, IntPtr gch)
		{
			DeviceChangeArgs args = new DeviceChangeArgs ();
			try {
				GLib.Signal sig = ((GCHandle)gch).Target as GLib.Signal;
				if (sig == null)
					throw new Exception ("Unknown signal GC handle received " + gch);

				args.Args = new object[1];
				args.Args [0] = arg1;
				GlibDeviceChangeHandler handler = (GlibDeviceChangeHandler)sig.Handler;
				handler (GLib.Object.GetObject (arg0), args);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DeviceChangeVMDelegate (IntPtr gcc,int deviceChange);

		static DeviceChangeVMDelegate DeviceChangeVMCallback;

		static void device_change_cb (IntPtr gcc, int deviceChange)
		{
			try {
				GstCameraCapturer gcc_managed = GLib.Object.GetObject (gcc, false) as GstCameraCapturer;
				gcc_managed.OnDeviceChange (deviceChange);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		private static void OverrideDeviceChange (GLib.GType gtype)
		{
			if (DeviceChangeVMCallback == null)
				DeviceChangeVMCallback = new DeviceChangeVMDelegate (device_change_cb);
			OverrideVirtualMethod (gtype, "device_change", DeviceChangeVMCallback);
		}

		[GLib.DefaultSignalHandler (Type = typeof(LongoMatch.Video.Capturer.GstCameraCapturer), ConnectionMethod = "OverrideDeviceChange")]
		protected virtual void OnDeviceChange (int deviceChange)
		{
			GLib.Value ret = GLib.Value.Empty;
			GLib.ValueArray inst_and_params = new GLib.ValueArray (2);
			GLib.Value[] vals = new GLib.Value [2];
			vals [0] = new GLib.Value (this);
			inst_and_params.Append (vals [0]);
			vals [1] = new GLib.Value (deviceChange);
			inst_and_params.Append (vals [1]);
			g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
			foreach (GLib.Value v in vals)
				v.Dispose ();
		}

		[GLib.Signal ("device_change")]
		public event GlibDeviceChangeHandler GlibDeviceChange {
			add {
				GLib.Signal sig = GLib.Signal.Lookup (this, "device_change", new DeviceChangeSignalDelegate (DeviceChangeSignalCallback));
				sig.AddDelegate (value);
			}
			remove {
				GLib.Signal sig = GLib.Signal.Lookup (this, "device_change", new DeviceChangeSignalDelegate (DeviceChangeSignalCallback));
				sig.RemoveDelegate (value);
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void EosVMDelegate (IntPtr gcc);

		static EosVMDelegate EosVMCallback;

		static void eos_cb (IntPtr gcc)
		{
			try {
				GstCameraCapturer gcc_managed = GLib.Object.GetObject (gcc, false) as GstCameraCapturer;
				gcc_managed.OnEos ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		private static void OverrideEos (GLib.GType gtype)
		{
			if (EosVMCallback == null)
				EosVMCallback = new EosVMDelegate (eos_cb);
			OverrideVirtualMethod (gtype, "eos", EosVMCallback);
		}

		[GLib.DefaultSignalHandler (Type = typeof(LongoMatch.Video.Capturer.GstCameraCapturer), ConnectionMethod = "OverrideEos")]
		protected virtual void OnEos ()
		{
			GLib.Value ret = GLib.Value.Empty;
			GLib.ValueArray inst_and_params = new GLib.ValueArray (1);
			GLib.Value[] vals = new GLib.Value [1];
			vals [0] = new GLib.Value (this);
			inst_and_params.Append (vals [0]);
			g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
			foreach (GLib.Value v in vals)
				v.Dispose ();
		}

		[GLib.Signal ("eos")]
		public event System.EventHandler Eos {
			add {
				GLib.Signal sig = GLib.Signal.Lookup (this, "eos");
				sig.AddDelegate (value);
			}
			remove {
				GLib.Signal sig = GLib.Signal.Lookup (this, "eos");
				sig.RemoveDelegate (value);
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void MediaInfoVMDelegate (IntPtr gcc,int width,int height,int par_n,int par_d);

		static MediaInfoVMDelegate MediaInfoVMCallback;

		static void media_info_cb (IntPtr gcc, int width, int height, int par_n, int par_d)
		{
			try {
				GstCameraCapturer gcc_managed = GLib.Object.GetObject (gcc, false) as GstCameraCapturer;
				gcc_managed.OnMediaInfo (width, height, par_n, par_d);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		private static void OverrideMediaInfo (GLib.GType gtype)
		{
			if (MediaInfoVMCallback == null)
				MediaInfoVMCallback = new MediaInfoVMDelegate (media_info_cb);
			OverrideVirtualMethod (gtype, "media-info", MediaInfoVMCallback);
		}

		[GLib.DefaultSignalHandler (Type = typeof(GstCameraCapturer), ConnectionMethod = "OverrideMediaInfo")]
		protected virtual void OnMediaInfo (int width, int height, int par_n, int par_d)
		{
			GLib.Value ret = GLib.Value.Empty;
			GLib.ValueArray inst_and_params = new GLib.ValueArray (6);
			GLib.Value[] vals = new GLib.Value [6];
			vals [0] = new GLib.Value (this);
			inst_and_params.Append (vals [0]);
			vals [1] = new GLib.Value (width);
			inst_and_params.Append (vals [1]);
			vals [2] = new GLib.Value (height);
			inst_and_params.Append (vals [2]);
			vals [3] = new GLib.Value (par_n);
			inst_and_params.Append (vals [3]);
			vals [4] = new GLib.Value (par_d);
			inst_and_params.Append (vals [4]);
			g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
			foreach (GLib.Value v in vals)
				v.Dispose ();
		}

		[GLib.Signal ("media-info")]
		public event GlibMediaInfoHandler GlibMediaInfo {
			add {
				GLib.Signal sig = GLib.Signal.Lookup (this, "media-info", typeof(MediaInfoArgs));
				sig.AddDelegate (value);
			}
			remove {
				GLib.Signal sig = GLib.Signal.Lookup (this, "media-info", typeof(MediaInfoArgs));
				sig.RemoveDelegate (value);
			}
		}

		#pragma warning restore 0169

		public void Configure (CaptureSettings settings, IntPtr window_handle)
		{
			IntPtr err = IntPtr.Zero;
			EncodingQuality qual = settings.EncodingSettings.EncodingQuality;
			Device device = settings.Device;
			DeviceVideoFormat format = settings.Format;
			EncodingProfile enc;
			VideoStandard std;
			IntPtr outFile, sourceElement, deviceID;
			
			enc = settings.EncodingSettings.EncodingProfile;
			std = settings.EncodingSettings.VideoStandard;
			
			outFile = Marshaller.StringToPtrGStrdup (settings.EncodingSettings.OutputFile);
			sourceElement = Marshaller.StringToPtrGStrdup (device.SourceElement);
			deviceID = Marshaller.StringToPtrGStrdup (device.ID);
			
			gst_camera_capturer_configure (Handle, outFile, (int)settings.Device.DeviceType,
				sourceElement, deviceID,
				format.width, format.height, format.fps_n, format.fps_d,
				(int)enc.VideoEncoder, (int)enc.AudioEncoder,
				(int)enc.Muxer, qual.VideoQuality,
				qual.AudioQuality,
				settings.EncodingSettings.EnableAudio,
				std.Width, std.Height, window_handle,
				out err);
			Marshaller.Free (outFile);
			Marshaller.Free (sourceElement);
			Marshaller.Free (deviceID);
			if (err != IntPtr.Zero)
				throw new GLib.GException (err);
		}

		public void Stop ()
		{
			timer.Stop ();
			gst_camera_capturer_stop (Handle);
		}

		public void TogglePause ()
		{
			timer.TogglePause ();
			gst_camera_capturer_toggle_pause (Handle);
		}

		public Time CurrentTime {
			get {
				return  new Time (timer.CurrentTime);
			}
		}

		public void Start ()
		{
			timer.Start ();
			gst_camera_capturer_start (Handle);
		}

		public void Run ()
		{
			gst_camera_capturer_run (Handle);
		}

		public void Close ()
		{
			gst_camera_capturer_close (Handle);
		}

		public void Expose ()
		{
			gst_camera_capturer_expose (Handle);
		}

		public Image CurrentFrame {
			get {
				IntPtr raw_ret = gst_camera_capturer_get_current_frame (Handle);
				Gdk.Pixbuf p = GLib.Object.GetObject (raw_ret) as Gdk.Pixbuf;
				/* The refcount for p is now 2. We need to decrease the counter to 1
				 * so that p.Dipose() sets it to 0 and triggers the pixbuf destroy function
				 * that frees the associated data*/
				gst_camera_capturer_unref_pixbuf (raw_ret);
				return new Image (p);
			}
		}

		public static new GLib.GType GType {
			get {
				IntPtr raw_ret = gst_camera_capturer_get_type ();
				GLib.GType ret = new GLib.GType (raw_ret);
				return ret;
			}
		}

		static GstCameraCapturer ()
		{
			LongoMatch.GtkSharp.Capturer.ObjectManager.Initialize ();
		}

		#endregion
		
	}
}
