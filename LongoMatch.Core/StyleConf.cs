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

namespace LongoMatch.Common
{
	public class StyleConf
	{
		public const int WelcomeBorder = 30;
		public const int WelcomeIconSize = 80;
		public const int WelcomeLogoWidth = 450;
		public const int WelcomeLogoHeight = 99;
		public const int WelcomeIconsHSpacing = 105;
		public const int WelcomeIconsVSpacing = 55;
		public const int WelcomeIconsTextSpacing = 5;
		public const int WelcomeIconsTextHeight = 20;
		public const int WelcomeIconsPerRow = 3;
		public const int WelcomeTextHeight = 20;
		public const int WelcomeMinWidthBorder = 30;

		public const int NewHeaderHeight = 60;
		public const int NewHeaderSpacing = 10;
		public const int NewEntryWidth = 150;
		public const int NewEntryHeight = 30;
		public const int NewTableHSpacing = 5;
		public const int NewTableVSpacing = 5;
		public const int NewTeamsComboWidth = 245;
		public const int NewTeamsComboHeight = 60;
		public const int NewTeamsIconSize = 55;
		public const string NewTeamsFont = "Ubuntu 16";
		public static Color NewTeamsFontColor = Color.White;
		public const int NewTeamsSpacing = 60;
		public const int NewTaggerSpacing = 35;
		
		public const string TimelineNeedleResource = "hicolor/scalable/actions/longomatch-timeline-needle-big.svg";
		public const string TimelineNeedleUP = "hicolor/scalable/actions/longomatch-timeline-needle-up.svg";
		public const int TimelineCategoryHeight = 20;
		public const int TimelineLabelsWidth = 150;
		public const int TimelineLabelHSpacing = 10;
		public const int TimelineLabelVSpacing = 2;
		public const int TimelineLineSize = 6;
		
		public int BenchLineWidth = 2;
		public int TeamTaggerBenchBorder = 10;
		
		public int PlayerSize = 60;
		public int PlayerBorder = 2;
		public int PlayerRadius = 2;
		public int PlayerTeamLineWidth = 2;
		public int PlayerNumberHeight  = 17;
		public int PlayerNumberWidth  = 26;
		public int PlayerNumberOffset  = 17;
		public int PlayerArrowOffset = 14; 
		public int PlayerArrowSize = 20; 

		public Color HomeTeamColor { get; set; }

		public Color AwayTeamColor { get; set; }

		public Color PaletteBackground { get; set; }

		public Color PaletteBackgroundLight { get; set; }

		public Color PaletteBackgroundDark { get; set; }

		public Color PaletteWidgets { get; set; }

		public Color PaletteSelected { get; set; }

		public Color PaletteActive { get; set; }

		public Color PaletteTool { get; set; }

		public static StyleConf Load (string filename)
		{
			return Serializer.Load <StyleConf> (filename);
		}
	}
}
