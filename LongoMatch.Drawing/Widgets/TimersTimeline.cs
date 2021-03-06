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
using System.Collections.Generic;
using System.Linq;
using LongoMatch.Core.Common;
using LongoMatch.Core.Handlers;
using LongoMatch.Core.Interfaces.Drawing;
using LongoMatch.Core.Store;
using LongoMatch.Core.Store.Drawables;
using LongoMatch.Drawing.CanvasObjects.Timeline;

namespace LongoMatch.Drawing.Widgets
{
	public class TimersTimeline: SelectionCanvas
	{
	
		public event TimeNodeChangedHandler TimeNodeChanged;
		public event ShowTimerMenuHandler ShowTimerMenuEvent;

		double secondsPerPixel;
		TimerTimeline timertimeline;
		Time duration;
		Dictionary <Timer, TimerTimeline> timers;

		public TimersTimeline (IWidget widget) : base (widget)
		{
			secondsPerPixel = 0.1;
			Accuracy = Constants.TIMELINE_ACCURACY;
			SelectionMode = MultiSelectionMode.MultipleWithModifier;
		}

		public void LoadPeriods (List<Period> periods, Time duration)
		{
			LoadTimers (periods.Select (p => p as Timer).ToList (), duration);
		}

		public void LoadTimers (List<Timer> timers, Time duration)
		{
			ClearObjects ();
			this.timers = new Dictionary<Timer, TimerTimeline> ();
			this.duration = duration;
			FillCanvas (timers);
			widget.ReDraw ();
		}

		public TimerTimeline TimerTimeline {
			get {
				return timertimeline;
			}
		}

		public Time CurrentTime {
			set {
				foreach (TimerTimeline tl in timers.Values) {
					tl.CurrentTime = value;
				}
			}
		}

		public double SecondsPerPixel {
			set {
				secondsPerPixel = value;
				Update ();
			}
			get {
				return secondsPerPixel;
			}
		}

		void Update ()
		{
			double width;

			if (duration == null)
				return;
			width = duration.TotalSeconds / SecondsPerPixel;
			widget.Width = width + 10;
			foreach (TimelineObject tl in timers.Values) {
				tl.Width = width + 10;
				tl.SecondsPerPixel = SecondsPerPixel;
			}
		}

		void FillCanvas (List<Timer> timers)
		{
			widget.Height = Constants.TIMER_HEIGHT;
			timertimeline = new TimerTimeline (timers, true, NodeDraggingMode.All, true, duration, 0,
				Config.Style.PaletteBackground,
				Config.Style.PaletteBackgroundLight);
			foreach (Timer t in timers) {
				this.timers [t] = timertimeline;
			}
			AddObject (timertimeline);
			Update ();
		}

		protected override void StartMove (Selection sel)
		{
			if (sel == null)
				return;

			if (sel.Position == SelectionPosition.All) {
				widget.SetCursor (CursorType.Selection);
			} else {
				widget.SetCursor (CursorType.DoubleArrow);
			}
		}

		protected override void StopMove (bool moved)
		{
			widget.SetCursor (CursorType.Arrow);
		}

		protected override void SelectionMoved (Selection sel)
		{
			if (TimeNodeChanged != null) {
				Time moveTime;
				TimeNode tn = (sel.Drawable as TimeNodeObject).TimeNode;

				if (sel.Position == SelectionPosition.Right) {
					moveTime = tn.Stop;
				} else {
					moveTime = tn.Start;
				}
				TimeNodeChanged (tn, moveTime);
			}
		}

		protected override void ShowMenu (Point coords)
		{
			if (ShowTimerMenuEvent != null) {
				Timer t = null;
				if (Selections.Count > 0) {
					TimerTimeNodeObject to = Selections.Last ().Drawable as TimerTimeNodeObject; 
					t = to.Timer;
				} 
				ShowTimerMenuEvent (t, Utils.PosToTime (coords, SecondsPerPixel));
			}
		}
	}
}
