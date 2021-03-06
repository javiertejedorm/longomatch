// 
//  Copyright (C) 2012 Andoni Morales Alastruey
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
using System.Collections.Generic;
using System.Drawing;
using Mono.Unix;
using OfficeOpenXml;
using OfficeOpenXml.Style;

using LongoMatch.Stats;
using LongoMatch.Store;

public class GameUnitsStatsSheet
{
	ProjectStats stats;
	ExcelWorksheet ws;
	
	public GameUnitsStatsSheet (ExcelWorksheet ws, ProjectStats stats)
	{
		this.stats = stats;
		this.ws = ws;
	}
	
	public void Fill() {
		int row = 1;
		
		SetColoredHeader(Catalog.GetString("Game Units"), 1, 1, 5);
		row++;
		
		
		row = FillGameStats (stats.GameUnitsStats, row);
		row = FillFirstLevelGameUnitsStats (stats.GameUnitsStats, row);
		row = FillGameUnitsStats (stats.GameUnitsStats, row);
	}
	
	void SetColoredHeader (string title, int row, int column, int width=1) {
		ws.Cells[row, column].Value = title;
		ws.Cells[row, column].Style.Fill.PatternType =  ExcelFillStyle.Solid;	
		ws.Cells[row, column].Style.Fill.BackgroundColor.SetColor(Color.CadetBlue);
		if (width > 1) {
		 ws.Cells[row, column, row, column + width - 1].Merge = true;	
		}
	}

	int FillGameStats (GameUnitsStats stats, int row)
	{
		Dictionary<GameUnit, GameUnitStatsNode> gameUnitsNodes  = stats.GameUnitNodes;

		SetColoredHeader(Catalog.GetString("Duration"), row, 2);
		SetColoredHeader(Catalog.GetString("Played Time"), row, 3);
		row++;
		foreach (GameUnit gu in gameUnitsNodes.Keys) {
			ws.Cells[row, 1].Value = Catalog.GetString("Match");
			ws.Cells[row, 2].Value = gameUnitsNodes[gu].Duration / 1000;
			ws.Cells[row, 3].Value = gameUnitsNodes[gu].PlayingTime / 1000;
			row++;
			break;
		}
		row ++;
		return row;
	}
	
	int FillFirstLevelGameUnitsStats (GameUnitsStats stats, int row) {
		int i=1;
		stats.GameNode.Sort((a, b) => (a.Node.Start - b.Node.Start).MSeconds);
		
		SetColoredHeader(Catalog.GetString("Duration"), row, 2);
		SetColoredHeader(Catalog.GetString("Played Time"), row, 3);
		row++;
		foreach (GameUnitStatsNode node in stats.GameNode){
			ws.Cells[row, 1].Value = node.Name + ' ' + i;
			ws.Cells[row, 2].Value = node.Duration / 1000;
			ws.Cells[row, 3].Value = node.PlayingTime / 1000;
			i++;
			row++;
		}
		row ++;
		return row;
	}
	
	int FillHeaders (int row) {
		SetColoredHeader(Catalog.GetString("Name"), row, 1);
		SetColoredHeader(Catalog.GetString("Count"), row, 2);
		SetColoredHeader(Catalog.GetString("Played Time"), row, 3);
		SetColoredHeader(Catalog.GetString("Average"), row, 4);
		SetColoredHeader(Catalog.GetString("Deviation"), row , 5);
		row ++;
		return row;
	}
	
	int FillGameUnitsStats (GameUnitsStats stats, int row) {
		row = FillHeaders(row);
		
		Dictionary<GameUnit, GameUnitStatsNode> gameUnitsNodes  = stats.GameUnitNodes;
		foreach (GameUnit gu in gameUnitsNodes.Keys) {
			row = FillStats (gu.Name, gameUnitsNodes[gu], row);
		}
		return row;
	}
	
	int FillStats (string name, GameUnitStatsNode guStats, int row) {
		ws.Cells[row, 1].Value = name;
		ws.Cells[row, 2].Value = guStats.Count;
		ws.Cells[row, 3].Value = guStats.PlayingTime / (float)1000;
		ws.Cells[row, 4].Value = guStats.PlayingTimeStdDeviation / 1000;
		ws.Cells[row, 5].Value = guStats.AveragePlayingTime / 1000;
		row ++;
		return row;
	}
	
}
