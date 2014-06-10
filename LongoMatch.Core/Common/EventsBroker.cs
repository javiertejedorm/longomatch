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
using LongoMatch.Handlers;
using System.Collections.Generic;
using LongoMatch.Store;
using LongoMatch.Interfaces;
using LongoMatch.Interfaces.GUI;

namespace LongoMatch.Common
{
	public class EventsBroker
	{
	
		public event NewTagHandler NewTagEvent;
		public event NewTagAtPosHandler NewTagAtPosEvent;
		public event NewTagStartHandler NewTagStartEvent;
		public event NewTagStopHandler NewTagStopEvent;
		public event NewTagCancelHandler NewTagCancelEvent;
		public event PlaysDeletedHandler PlaysDeleted;
		public event PlaySelectedHandler PlaySelected;
		public event PlayCategoryChangedHandler PlayCategoryChanged;
		public event PlayListNodeAddedHandler PlayListNodeAdded;
		public event TimeNodeChangedHandler TimeNodeChanged;
		public event SnapshotSeriesHandler SnapshotSeries;
		public event TagPlayHandler TagPlay;
		public event DuplicatePlayHandler DuplicatePlay;
		public event TeamsTagsChangedHandler TeamTagsChanged;
		
		/* Playlist */
		public event RenderPlaylistHandler RenderPlaylist;
		public event PlayListNodeAddedHandler PlayListNodeAddedEvent;
		public event PlayListNodeSelectedHandler PlayListNodeSelectedEvent;
		public event OpenPlaylistHandler OpenPlaylistEvent;
		public event NewPlaylistHandler NewPlaylistEvent;
		public event SavePlaylistHandler SavePlaylistEvent; 
		
		
		public event KeyHandler KeyPressed;
		
		/* Project options */
		public event SaveProjectHandler SaveProjectEvent;
		public event CloseOpenendProjectHandler CloseOpenedProjectEvent;
		public event ShowFullScreenHandler ShowFullScreenEvent;
		public event ShowProjectStats ShowProjectStatsEvent;
		public event TagSubcategoriesChangedHandler TagSubcategoriesChangedEvent;
		
		/* IMainController */
		public event NewProjectHandler NewProjectEvent;
		public event OpenNewProjectHandler OpenNewProjectEvent;
		public event OpenProjectHandler OpenProjectEvent;
		public event OpenProjectIDHandler OpenProjectIDEvent;
		public event ImportProjectHandler ImportProjectEvent;
		public event ExportProjectHandler ExportProjectEvent;
		public event QuitApplicationHandler QuitApplicationEvent;
		public event ManageJobsHandler ManageJobsEvent; 
		public event ManageTeamsHandler ManageTeamsEvent;
		public event ManageCategoriesHandler ManageCategoriesEvent;
		public event ManageProjects ManageProjectsEvent;
		public event ManageDatabases ManageDatabasesEvent;
		public event EditPreferences EditPreferencesEvent;
		public event ConvertVideoFilesHandler ConvertVideoFilesEvent;
		
		public event OpenedProjectChangedHandler OpenedProjectChanged;
		
		public event TickHandler Tick;
		public event ErrorHandler MultimediaError;
		public event SegmentClosedHandler SegmentClosed;
		
		
		public EventsBroker ()
		{
		}
		
		public void EmitNewTagAtPos(Category category, Time pos) {
			if (NewTagAtPosEvent != null)
				NewTagAtPosEvent(category, pos);
		}

		public void EmitNewTag(Category category) {
			if (NewTagEvent != null)
				NewTagEvent(category);
		}

		public void EmitNewTagStart(Category category) {
			if (NewTagStartEvent != null)
				NewTagStartEvent (category);
		}

		public void EmitNewTagStop(Category category) {
			if (NewTagStopEvent != null)
				NewTagStopEvent (category);
		}
		
		public void EmitNewTagCancel(Category category) {
			if (NewTagCancelEvent != null)
				NewTagCancelEvent (category);
		}
		
		
		public void EmitPlaysDeleted(List<Play> plays)
		{
			if (PlaysDeleted != null)
				PlaysDeleted(plays);
		}
		
		public void EmitPlaySelected(Play play)
		{
			if (PlaySelected != null)
				PlaySelected(play);
		}
		
		public void EmitSnapshotSeries(Play play)
		{
			if (SnapshotSeries != null)
				SnapshotSeries(play);
		}
		
		public void EmitRenderPlaylist(IPlayList playlist) {
			if (RenderPlaylist != null)
				RenderPlaylist(playlist);
		}
		
		public void EmitPlayListNodeAdded(List<Play> plays)
		{
			if (PlayListNodeAdded != null)
				PlayListNodeAdded(plays);
		}
		
		public void EmitTimeNodeChanged (TimeNode tn, object val)
		{
			if (TimeNodeChanged != null)
				TimeNodeChanged(tn, val);
		}
		
		public virtual void EmitPlayCategoryChanged(Play play, Category cat)
		{
			if(PlayCategoryChanged != null)
				PlayCategoryChanged(play, cat);
		}
		
		public void EmitTagPlay(Play play) {
			if (TagPlay != null)
				TagPlay (play);
		}

		public void EmitDuplicatePlay (Play play)
		{
			if (DuplicatePlay != null)
				DuplicatePlay (play);
		}
		
		public void EmitNewPlaylist() {
			if (NewPlaylistEvent != null)
				NewPlaylistEvent();
		}
		
		public void EmitOpenPlaylist() {
			if (OpenPlaylistEvent != null)
				OpenPlaylistEvent();
		}
		
		public void EmitSavePlaylist() {
			if (SavePlaylistEvent != null)
				SavePlaylistEvent();
		}
		
		public void EmitKeyPressed(object sender, int key, int modifier) {
			if (KeyPressed != null)
				KeyPressed(sender, key, modifier);
		}
		
		public void EmitCloseOpenedProject () {
			if (CloseOpenedProjectEvent != null)
				CloseOpenedProjectEvent ();
		}
		
		public void EmitShowProjectStats (Project project) {
			if (ShowProjectStatsEvent != null)
				ShowProjectStatsEvent (project);
		}
		
		public void EmitTagSubcategories (bool active) {
			if (TagSubcategoriesChangedEvent != null)
				TagSubcategoriesChangedEvent (active);
		}

		public void EmitShowFullScreen (bool active)
		{
			if (ShowFullScreenEvent != null) {
				ShowFullScreenEvent (active);
			}
		}
		
		public void EmitSaveProject (Project project, ProjectType projectType) {
			if (SaveProjectEvent != null)
				SaveProjectEvent (project, projectType);
		}
		
		public void EmitNewProject () {
			if (NewProjectEvent != null)
				NewProjectEvent();
		}
		
		public void EmitOpenProject () {
			if(OpenProjectEvent != null)
				OpenProjectEvent();
		}
				
		public void EmitEditPreferences ()
		{
			if (EditPreferencesEvent != null)
				EditPreferencesEvent();
		}
		
		public void EmitManageJobs() {
			if(ManageJobsEvent != null)
				ManageJobsEvent();
		}
		
		public void EmitManageTeams() {
			if(ManageTeamsEvent != null)
				ManageTeamsEvent();
		}
		
		public void EmitManageProjects()
		{
			if (ManageProjectsEvent != null)
				ManageProjectsEvent();
		}
		
		public void EmitManageDatabases()
		{
			if (ManageDatabasesEvent != null)
				ManageDatabasesEvent();
		}
		
		public void EmitManageCategories() {
			if(ManageCategoriesEvent != null)
				ManageCategoriesEvent();
		}
		
		public void EmitImportProject(string name, string filterName, string filter,
		                               Func<string, Project> func, bool requiresNewFile) {
			if (ImportProjectEvent != null)
				ImportProjectEvent(name, filterName, filter, func, requiresNewFile);
		}
		
		public void EmitExportProject (Project project) {
			if(ExportProjectEvent != null)
				ExportProjectEvent (project);
		}
		
		public void EmitOpenProjectID (Guid projectID ) {
			if (OpenProjectIDEvent != null) {
				OpenProjectIDEvent (projectID);
			}
		}
		
		public void EmitOpenNewProject (Project project, ProjectType projectType, CaptureSettings captureSettings)
		{
			if (OpenNewProjectEvent != null) {
				OpenNewProjectEvent (project, projectType, captureSettings);
			}
		}
		
		public void EmitConvertVideoFiles (List<MediaFile> files, EncodingSettings settings) {
			if (ConvertVideoFilesEvent != null)
				ConvertVideoFilesEvent (files, settings);
		}
		
		public void EmitQuitApplication () {
			if (QuitApplicationEvent != null) {
				QuitApplicationEvent ();
			}
		}
		
		public  void EmitOpenedProjectChanged (Project project, ProjectType projectType,
		                                       PlaysFilter filter, IAnalysisWindow analysisWindow)
		{
			if (OpenedProjectChanged != null) {
				OpenedProjectChanged (project, projectType, filter, analysisWindow);
			}
		}

		public void EmitTick (Time currentTime, Time streamLength, double currentPosition)
		{
			if (Tick != null) {
				Tick (currentTime, streamLength, currentPosition);
			}
		}
		
		public void EmitSegmentClosed ()
		{
			if (SegmentClosed != null) {
				SegmentClosed ();
			}
		}
		
		public void EmitTeamTagsChanged ()
		{
			if (TeamTagsChanged != null) {
				TeamTagsChanged ();
			}
		}
	}
}
