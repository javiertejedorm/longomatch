// PixbufTimeNode.cs
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
using System.IO;

using LongoMatch.Common;

namespace LongoMatch.Store
{

	/// <summary>
	/// Base class for all the video segments containing a snapshot
	/// It has a <see cref="Gdk.Pixbuf"/> with a thumbnail of the video segment.
	/// </summary>
	[Serializable]
	public class PixbufTimeNode : TimeNode
	{
		private byte[] thumbnailBuf;
		
		#region Contructors
		public PixbufTimeNode() {
		}
		#endregion

		#region Properties
		/// <summary>
		/// Segment thumbnail
		/// </summary>
		public Image Miniature {
			get {
				if(thumbnailBuf != null)
					return Image.Deserialize(thumbnailBuf);
				else return null;
			} set {
				if (value == null)
					thumbnailBuf = null;
				else {
					if (value.Height > Constants.MAX_THUMBNAIL_SIZE ||
					    value.Width > Constants.MAX_THUMBNAIL_SIZE) {
						value.Scale(Constants.MAX_THUMBNAIL_SIZE, Constants.MAX_THUMBNAIL_SIZE);
					}
					thumbnailBuf = value.Serialize();
				}
			}
		}
		
		#endregion
	}
}
