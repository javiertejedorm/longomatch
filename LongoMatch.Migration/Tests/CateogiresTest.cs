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
using NUnit.Framework;
using System;
using System.Reflection;
using LongoMatch.Store.Templates;
using LongoMatch.Common;
using System.IO;

namespace LongoMatch.Migration.Tests
{
	[TestFixture()]
	public class CateogiresTest
	{
		[Test()]
		public void TestCategoriesMigration ()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "default.lct";
			
			using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
				Categories cats = SerializableObject.Load<Categories> (stream, SerializationType.Binary);
				var cstream = new MemoryStream ();
				SerializableObject.Save (cats, cstream, SerializationType.Json);
				cstream.Seek (0, SeekOrigin.Begin);
				var jsonString = new StreamReader(cstream).ReadToEnd();
				Console.WriteLine (jsonString);
			}
		}
	}
}