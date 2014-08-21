//
//  Copyright (C) 2009 Andoni Morales Alastruey
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
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//

using System;

namespace LongoMatch.Common
{

	public enum SerializationType {
		Binary,
		Xml,
		Json
	}
		

	public enum ProjectType {
		CaptureProject,
		URICaptureProject,
		FakeCaptureProject,
		FileProject,
		EditProject,
		None,
	}
	
	public enum CapturerType {
		Fake,
		Live,
	}

	public enum EndCaptureResponse {
		Return = 234,
		Quit = 235,
		Save = 236
	}

	public enum TagMode {
		Predefined,
		Free,
		Edit
	}

	public enum SortMethodType {
		SortByName = 0,
		SortByStartTime = 1,
		SortByStopTime = 2,
		SortByDuration = 3
	}

	public enum Team {
		NONE = 0,
		LOCAL = 1,
		VISITOR = 2,
		BOTH = 3,
	}
	
	public enum JobState {
		NotStarted,
		Running,
		Finished,
		Cancelled,
		Error,
	}
	
	public enum VideoEncoderType {
		Mpeg4,
		Xvid,
		Theora,
		H264,
		Mpeg2,
		VP8,
	}

	public enum AudioEncoderType {
		Mp3,
		Aac,
		Vorbis,
	}

	public enum VideoMuxerType {
		Avi,
		Mp4,
		Matroska,
		Ogg,
		MpegPS,
		WebM,
	}
	
	public enum DrawTool {
		Pen,
		Line,
		Ellipse,
		Rectangle,
		Angle,
		Cross,
		Eraser,
		Selection,
		RectangleArea,
		CircleArea,
		Player,
		Text,
		Counter,
	}
	
	public enum CaptureSourceType {
		None,
		DV,
		System,
		URI,
	}
	
	public enum GameUnitEventType {
		Start, 
		Stop,
		Cancel
	}
	
	public enum EditorState
	{
		START = 0,
		FINISHED = 1,
		CANCELED = -1,
		ERROR = -2
	}
	
	public enum JobType {
		VideoEdition,
		VideoConversion
	}
	
	public enum VideoAnalysisMode {
		PredefinedTagging,
		ManualTagging,
		Timeline,
		GameUnits,
	}
	
	public enum SelectionPosition {
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
		Left,
		Right,
		Top,
		Bottom,
		LineStart,
		LineStop,
		AngleStart,
		AngleStop,
		AngleCenter,
		CircleBorder,
		All,
	}
	
	public enum LineStyle {
		Normal,
		Dashed,
		Pointed
	}
	
	public enum LineType {
		Simple,
		Arrow,
		DoubleArrow,
		Dot,
		DoubleDot
	}
	
	public enum FontSlant {
		Italic,
		Normal,
		Oblique,
	}
	
	public enum FontWeight {
		Normal,
		Bold
	}
	
	public enum ButtonType {
		None,
		Left,
		Center,
		Right
	}
	
	public enum ButtonModifier {
		None,
		Shift,
		Control
	}
	
	public enum CursorType {
		Arrow,
		DoubleArrow,
		Selection,
		Cross,
	}
	
	public enum MultiSelectionMode {
		Single,
		Multiple,
		MultipleWithModifier,
	}
	
	public enum PlayersIconSize {
		Smallest = 20,
		Small = 30,
		Medium = 40,
		Large = 50,
		ExtraLarge = 60
	}
	
	public enum FieldPositionType {
		Field,
		HalfField,
		Goal
	}
	
	public enum CardShape {
		Rectangle,
		Triangle,
		Circle
	}
	
	public enum FitMode {
		Fill,
		Fit,
		Original
	}
}

