//
//  Copyright (C) 2007-2009 Andoni Morales Alastruey
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

using Gdk;
using Gtk;
using LongoMatch.Core.Common;
using LongoMatch.Core.Store;
using Misc = LongoMatch.Gui.Helpers.Misc;

namespace LongoMatch.Gui.Component
{


	[System.ComponentModel.Category ("LongoMatch")]
	[System.ComponentModel.ToolboxItem (true)]
	public partial class PlayersTreeView : ListTreeViewBase
	{

		TreePath pathClicked;

		public PlayersTreeView ()
		{
			this.Team = TeamType.LOCAL;
		}

		public TeamType Team {
			set;
			get;
		}

		protected override int SortFunction (TreeModel model, TreeIter a, TreeIter b)
		{
			object oa;
			object ob;

			if (model == null)
				return 0;

			oa = model.GetValue (a, 0);
			ob = model.GetValue (b, 0);

			if (oa == null && ob == null) {
				return 0;
			} else if (oa == null) {
				return -1;
			} else if (ob == null) {
				return 1;
			}

			if (oa is Player)
				return (oa as Player).Number.CompareTo ((ob as Player).Number);
			else
				return (oa as TimeNode).Start.CompareTo ((ob as TimeNode).Start);
		}

		override protected bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			return false;
		}

		protected override bool OnButtonReleaseEvent (Gdk.EventButton evnt)
		{
			if (pathClicked != null) {
				if (GetRowExpanded (pathClicked)) {
					CollapseRow (pathClicked);
				} else {
					ExpandRow (pathClicked, true);
				}
				pathClicked = null;
			}
			return base.OnButtonReleaseEvent (evnt);
		}

		override protected bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			TreePath[] paths = Selection.GetSelectedRows ();

			if (Misc.RightButtonClicked (evnt)) {
				// We don't want to unselect the play when several
				// plays are selected and we clik the right button
				// For multiedition
				if (paths.Length <= 1) {
					base.OnButtonPressEvent (evnt);
					paths = Selection.GetSelectedRows ();
				}

				if (paths.Length == 1) {
					TimeNode selectedTimeNode = GetValueFromPath (paths [0]) as TimeNode;
					if (selectedTimeNode is TimelineEvent) {
						ShowMenu ();
					}
				} else if (paths.Length > 1) {
					ShowMenu ();
				}
			} else {
				GetPathAtPos ((int)evnt.X, (int)evnt.Y, out pathClicked);
				base.OnButtonPressEvent (evnt);
			}
			return true;
		}

		override protected bool SelectFunction (TreeSelection selection, TreeModel model, TreePath path, bool selected)
		{
			// Don't allow multiselection for Players
			if (!selected && selection.GetSelectedRows ().Length > 0) {
				if (selection.GetSelectedRows ().Length == 1 &&
				    GetValueFromPath (selection.GetSelectedRows () [0]) is Player)
					return false;
				return !(GetValueFromPath (path) is Player);
			}
			// Always unselect
			else
				return true;
		}
	}
}
