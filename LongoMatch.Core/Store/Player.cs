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

using System;
using LongoMatch.Common;
using Newtonsoft.Json;
using LongoMatch.Interfaces;

namespace LongoMatch.Store
{
	/// <summary>
	/// Player of a team
	/// </summary>
	[Serializable]
	public class Player: IIDObject
	{

		#region Constructors
		public Player()
		{
			ID = Guid.NewGuid ();
		}
		
		#endregion

		#region Properties
		
		public Guid ID {
			get;
			set;
		}
		
		/// <summary>
		/// My name
		/// </summary>
		public string Name {
			get;
			set;
		}
		
		public string LastName {
			get;
			set;
		}
		
		public string NickName {
			get;
			set;
		}

		/// <summary>
		/// My position in the field
		/// </summary>
		public string Position {
			get;
			set;
		}

		/// <summary>
		/// My shirt number
		/// </summary>
		public int Number {
			get;
			set;
		}

		/// <summary>
		/// My photo
		/// </summary>
		public Image Photo {
			get;
			set;
		}

		/// <summary>
		/// Date of birth
		/// </summary>
		public DateTime Birthday {
			get;
			set;
		}

		/// <summary>
		/// Nationality
		/// </summary>
		public String Nationality {
			get;
			set;
		}

		/// <summary>
		/// Height
		/// </summary>
		public float Height {
			get;
			set;
		}

		/// <summary>
		/// Weight
		/// </summary>
		public int Weight {
			get;
			set;
		}

		/// <summary>
		/// Whether this player is playing or not and shouldn't be added the
		/// list of taggable players
		/// </summary>
		public bool Playing {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the player e-mail.
		/// </summary>
		/// <value>
		/// The e-mail.
		/// </value>
		public string Mail {
			get;
			set;
		}
		
		public override string ToString ()
		{
			string displayName;
			
			if (NickName != null) {
				displayName = NickName;
			} else {
				displayName = Name + " " + LastName;
			}
			return String.Format("{0} - {1}", Number, displayName);
		}

		#endregion
	}
}
