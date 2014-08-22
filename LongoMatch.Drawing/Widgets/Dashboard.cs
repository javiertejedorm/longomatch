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
using System;
using System.Linq;
using LongoMatch.Store.Templates;
using System.Collections.Generic;
using LongoMatch.Common;
using LongoMatch.Drawing.CanvasObjects;
using LongoMatch.Handlers;
using LongoMatch.Store;
using LongoMatch.Store.Drawables;
using LongoMatch.Interfaces.Drawing;
using LongoMatch.Interfaces;

namespace LongoMatch.Drawing.Widgets
{
	public class Dashboard: SelectionCanvas
	{
	
		public event TaggersSelectedHandler TaggersSelectedEvent;
		public event TaggerSelectedHandler AddNewTagEvent;
		public event ShowButtonsTaggerMenuHandler ShowMenuEvent;
		public event NewTagHandler NewTagEvent;

		Categories template;
		TagMode tagMode;
		Time currentTime;
		int templateWidth, templateHeight;

		public Dashboard (IWidget widget): base (widget)
		{
			Accuracy = 5;
			TagMode = TagMode.Edit;
			widget.SizeChangedEvent += SizeChanged;
			FitMode = FitMode.Fit;
			AddTag = new Tag ("", "");
		}

		public Categories Template {
			set {
				template = value;
				LoadTemplate ();
			}
		}

		public Tag AddTag {
			get;
			set;
		}

		public bool Edited {
			get;
			set;
		}

		public Time CurrentTime {
			set {
				currentTime = value;
				foreach (TimerObject to in Objects.OfType<TimerObject>()) {
					to.CurrentTime = value;
				}
			}
			get {
				return currentTime;
			}
		}

		public TagMode TagMode {
			set {
				tagMode = value;
				ObjectsCanMove = tagMode == TagMode.Edit;
			}
			get {
				return tagMode;
			}
		}

		public FitMode FitMode {
			set;
			get;
		}

		public void Refresh (TaggerButton b)
		{
			TaggerObject to;
			
			LoadTemplate ();
			to = (TaggerObject)Objects.FirstOrDefault (o => (o as TaggerObject).Tagger == b);
			if (to != null) {
				UpdateSelection (new Selection (to, SelectionPosition.All, 0));
			}
		}

		protected override void ShowMenu (Point coords)
		{
			Selection sel;
			if (ShowMenuEvent == null)
				return;
			
			sel = Selections.LastOrDefault ();
			if (sel != null) {
				TaggerObject to = sel.Drawable as TaggerObject;
				Tag tag = null;

				if (to is CategoryObject) {
					tag = (to as CategoryObject).GetTagForCoords (coords);
				}
				ShowMenuEvent (to.Tagger, tag);
			}
		}

		protected override void SelectionMoved (Selection sel)
		{
			SizeChanged ();
			Edited = true;
			base.SelectionMoved (sel);
		}

		protected override void SelectionChanged (List<Selection> sel)
		{
			List<TaggerButton> taggers;
			
			taggers = sel.Select (s => (s.Drawable as TaggerObject).Tagger).ToList ();
			if (TagMode == TagMode.Edit) {
				if (TaggersSelectedEvent != null) {
					TaggersSelectedEvent (taggers);
				}
			}
			base.SelectionChanged (sel);
		}

		protected override void StopMove ()
		{
			Selection sel = Selections.FirstOrDefault ();
			
			if (sel != null) {
				int i = Constants.CATEGORY_TPL_GRID;
				TaggerButton tb = (sel.Drawable as TaggerObject).Tagger;
				tb.Position.X = Utils.Round (tb.Position.X, i);
				tb.Position.Y = Utils.Round (tb.Position.Y, i);
				tb.Width = (int)Utils.Round (tb.Width, i);
				tb.Height = (int)Utils.Round (tb.Height, i);
				widget.ReDraw ();
			}

			base.StopMove ();
		}

		public override void Draw (IContext context, Area area)
		{
			tk.Context = context;
			tk.Begin ();
			tk.Clear (Config.Style.PaletteBackground);
			if (TagMode == TagMode.Edit) {
				tk.TranslateAndScale (translation, new Point (scaleX, scaleY));
				/* Draw grid */
				tk.LineWidth = 1;
				tk.StrokeColor = Color.Grey1;
				tk.FillColor = Color.Grey1;
				/* Vertical lines */
				for (int i = 0; i <= templateHeight; i += Constants.CATEGORY_TPL_GRID) {
					tk.DrawLine (new Point (0, i), new Point (templateWidth, i));
				}
				/* Horizontal lines */
				for (int i = 0; i < templateWidth; i += Constants.CATEGORY_TPL_GRID) {
					tk.DrawLine (new Point (i, 0), new Point (i, templateHeight));
				}
			}
			tk.End ();
			
			base.Draw (context, area);
		}

		void LoadTemplate ()
		{
			foreach (CanvasObject co in Objects) {
				co.Dispose ();
			}
			Objects.Clear ();
			foreach (TagButton tag in template.CommonTags) {
				TagObject to = new TagObject (tag);
				to.ClickedEvent += HandleTaggerClickedEvent;
				to.Mode = TagMode;
				Objects.Add (to);
			}
			
			foreach (Category cat in template.CategoriesList) {
				CategoryObject co = new CategoryObject (cat);
				co.ClickedEvent += HandleTaggerClickedEvent;
				co.Mode = TagMode;
				co.AddTag = AddTag;
				Objects.Add (co);
			}

			foreach (PenaltyCard c in template.PenaltyCards) {
				CardObject co = new CardObject (c);
				co.ClickedEvent += HandleTaggerClickedEvent;
				co.Mode = TagMode;
				Objects.Add (co);
			}
			foreach (Score s in template.Scores) {
				ScoreObject co = new ScoreObject (s);
				co.ClickedEvent += HandleTaggerClickedEvent;
				co.Mode = TagMode;
				Objects.Add (co);
			}

			foreach (Timer t in template.Timers) {
				TimerObject to = new TimerObject (t);
				to.ClickedEvent += HandleTaggerClickedEvent;
				to.Mode = TagMode;
				Objects.Add (to);
			}
			Edited = false;
			SizeChanged ();
			widget.ReDraw ();
		}

		void SizeChanged ()
		{
			templateHeight = template.CanvasHeight;
			templateWidth = template.CanvasWidth;
			if (FitMode == FitMode.Original) {
				widget.Width = templateWidth;
				widget.Height = templateHeight;
			} else if (FitMode == FitMode.Fill) {
				scaleX = (double)widget.Width / templateWidth;
				scaleY = (double)widget.Height / templateHeight;
			} else if (FitMode == FitMode.Fit) {
				Image.ScaleFactor (templateWidth, templateHeight,
				                   (int)widget.Width, (int)widget.Height,
				                   out scaleX, out scaleY, out translation);
			}
		}

		void HandleTaggerClickedEvent (CanvasObject co)
		{
			TaggerObject tagger;
			Time start = null, stop = null;
			List<Tag> tags = null;
			
			tagger = co as TaggerObject;
			if (NewTagEvent == null || tagger is TimerObject ||
				tagger is TagObject) {
				return;
			}
			
			if (TagMode == TagMode.Edit) {
				if (tagger is CategoryObject) {
					if ((tagger as CategoryObject).SelectedTags.Contains (AddTag)) {
						AddNewTagEvent (tagger.Tagger);
					}
				}
				return;
			}
			
			if (tagger.Tagger.TagMode == TagMode.Predefined) {
				stop = CurrentTime + tagger.Tagger.Stop;
				start = CurrentTime - tagger.Tagger.Start;
			} else {
				stop = CurrentTime;
				start = tagger.Start - tagger.Tagger.Start;
			}
			
			if (tagger is CategoryObject) {
				tags = new List<Tag> ();
				tags.AddRange ((tagger as CategoryObject).SelectedTags);
				foreach (TagObject to in Objects.OfType<TagObject>()) {
					if (to.Active) {
						tags.Add (to.TagButton.Tag);
					}
					to.Active = false;
				}
			}
			NewTagEvent (tagger.Tagger, null, tags, start, stop);
		}
	}
}

