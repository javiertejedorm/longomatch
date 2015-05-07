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
using System.Linq;
using Gtk;
using Mono.Unix;

using LongoMatch.Core.Interfaces;
using LongoMatch.Gui.Helpers;
using LongoMatch.Core.Interfaces.GUI;
using System.Threading.Tasks;
using LongoMatch.Core.Common;

namespace LongoMatch.Gui.Dialog
{
	public partial class DatabasesManager : Gtk.Dialog
	{
		IDataBaseManager manager;
		ListStore store;

		public DatabasesManager (Window parent)
		{
			TransientFor = parent;
			this.Build ();
			this.manager = Config.DatabaseManager;
			ActiveDB = manager.ActiveDB;
			SetTreeView ();
			rescanbutton.Clicked += HandleRescanClicked;
		}

		IDatabase ActiveDB {
			set {
				dblabel.Text = Catalog.GetString ("Active database") + ": " + value.Name;
			}
		}

		void SetTreeView ()
		{
			/* DB name */
			TreeViewColumn nameCol = new TreeViewColumn ();
			nameCol.Title = Catalog.GetString ("Name");
			CellRendererText nameCell = new CellRendererText ();
			nameCol.PackStart (nameCell, true);
			nameCol.SetCellDataFunc (nameCell, new TreeCellDataFunc (RenderName));
			treeview.AppendColumn (nameCol);
			
			/* DB last backup */
			TreeViewColumn lastbackupCol = new TreeViewColumn ();
			lastbackupCol.Title = Catalog.GetString ("Last backup");
			CellRendererText lastbackupCell = new CellRendererText ();
			lastbackupCol.PackStart (lastbackupCell, true);
			lastbackupCol.SetCellDataFunc (lastbackupCell, new TreeCellDataFunc (RenderLastbackup));
			treeview.AppendColumn (lastbackupCol);
			
			/* DB Projects count */
			TreeViewColumn countCol = new TreeViewColumn ();
			countCol.Title = Catalog.GetString ("Projects count");
			CellRendererText countCell = new CellRendererText ();
			countCol.PackStart (countCell, true);
			countCol.SetCellDataFunc (countCell, new TreeCellDataFunc (RenderCount));
			treeview.AppendColumn (countCol);
			store = new ListStore (typeof(IDatabase));
			foreach (IDatabase db in manager.Databases) {
				store.AppendValues (db);
			}
			treeview.Model = store;
		}

		IDatabase SelectedDB {
			get {
				TreeIter iter;
				
				treeview.Selection.GetSelected (out iter);
				return store.GetValue (iter, 0) as IDatabase;
			}
		}

		void RenderCount (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			IDatabase db = (IDatabase)store.GetValue (iter, 0);

			(cell as Gtk.CellRendererText).Text = db.Count.ToString (); 
			if (db == manager.ActiveDB) {
				cell.CellBackground = "red";
			} else {
				cell.CellBackgroundGdk = LongoMatch.Gui.Helpers.Misc.ToGdkColor (Config.Style.PaletteBackground);
			}
		}

		void RenderLastbackup (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			IDatabase db = (IDatabase)store.GetValue (iter, 0);

			(cell as Gtk.CellRendererText).Text = db.LastBackup.ToShortDateString (); 
			if (db == manager.ActiveDB) {
				cell.CellBackground = "red";
			} else {
				cell.CellBackgroundGdk = LongoMatch.Gui.Helpers.Misc.ToGdkColor (Config.Style.PaletteBackground);
			}
		}

		void RenderName (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			IDatabase db = (IDatabase)store.GetValue (iter, 0);

			(cell as Gtk.CellRendererText).Text = db.Name; 
			if (db == manager.ActiveDB) {
				cell.CellBackground = "red";
			} else {
				cell.CellBackgroundGdk = LongoMatch.Gui.Helpers.Misc.ToGdkColor (Config.Style.PaletteBackground);
			}
		}

		protected void OnSelectbuttonClicked (object sender, System.EventArgs e)
		{
			IDatabase db = SelectedDB;
			if (db != null) {
				manager.ActiveDB = db;
				ActiveDB = db;
			}
		}

		protected void OnAddbuttonClicked (object sender, System.EventArgs e)
		{
			IDatabase db;
			string dbname = MessagesHelpers.QueryMessage (this, Catalog.GetString ("Database name"));
			if (dbname == null || dbname == "")
				return;
			
			if (manager.Databases.Where (d => d.Name == dbname).Count () != 0) {
				var msg = Catalog.GetString ("A database already exists with this name");
				MessagesHelpers.ErrorMessage (this, msg);
				return;
			}
			
			db = manager.Add (dbname);
			if (db != null)
				store.AppendValues (db);
		}

		protected void OnDelbuttonClicked (object sender, System.EventArgs e)
		{
			TreeIter iter;
			IDatabase db;
				
			treeview.Selection.GetSelected (out iter);
			db = store.GetValue (iter, 0) as IDatabase;
			
			if (db == manager.ActiveDB) {
				var msg = Catalog.GetString ("This database is the active one and can't be deleted");
				MessagesHelpers.ErrorMessage (this, msg);
				return;
			}

			if (db != null) {
				var msg = Catalog.GetString ("Do you really want to delete the database: " + db.Name);
				if (MessagesHelpers.QuestionMessage (this, msg)) {
					db.Backup ();
					manager.Delete (db);
					store.Remove (ref iter);
				}
			}
		}

		protected void OnBackupbuttonClicked (object sender, System.EventArgs e)
		{
			IDatabase db = SelectedDB;
			if (db != null) {
				if (db.Backup ())
					MessagesHelpers.InfoMessage (this, Catalog.GetString ("Backup successful"));
				else
					MessagesHelpers.ErrorMessage (this, Catalog.GetString ("Could not create backup"));
			}
		}

		protected void OnTreeviewCursorChanged (object sender, System.EventArgs e)
		{
			bool selected = SelectedDB != null;
			
			delbutton.Sensitive = selected;
			backupbutton.Sensitive = selected;
			selectbutton.Sensitive = selected;
			rescanbutton.Sensitive = selected;
		}

		void HandleRescanClicked (object sender, EventArgs e)
		{
			IDatabase db = SelectedDB;
			if (db != null) {
				IBusyDialog busy = Config.GUIToolkit.BusyDialog (Catalog.GetString ("Scanning database..."), this);
				Task task = new Task (() => {
					try {
						db.Reload ();
					} catch (Exception ex) {
						Log.Exception (ex);
					}
					Config.GUIToolkit.Invoke (delegate {
						busy.Destroy ();
					});
				});
				task.Start ();
				busy.ShowSync ();
				Config.GUIToolkit.InfoMessage (Catalog.GetString ("Database scanned succesfully."));
			}
		}
		
	}
}

