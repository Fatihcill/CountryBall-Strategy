using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    private int id;
    private ItemsDatabase database;
    private BuildingPlacer _buildingPlacer;    
    private GridData gridData;
    private CellIndicator cellIndicator;
    
    public PlacementState(int id, GridData gridData, ItemsDatabase database, BuildingPlacer buildingPlacer, CellIndicator cellIndicator)
    {
        this.id = id;
        this.database = database;
        this._buildingPlacer = buildingPlacer;
        this.gridData = gridData;
        this.cellIndicator = cellIndicator;
        selectedObjectIndex = database.itemPreviewData.FindIndex(data => data.id == id);
        if (selectedObjectIndex > -1)
        {
            cellIndicator.SetSprite(database.itemPreviewData[selectedObjectIndex].preview);
            cellIndicator.cellOffset = (Vector2)database.itemPreviewData[selectedObjectIndex].size / 2f;
        }
        else
            throw new System.Exception("No object with id " + id + " found");
    }

    public void EndState()
    {
        cellIndicator.SetSprite();
    }

    public void OnAction(Vector3Int cellPos)
    {
        if (!gridData.CanPlaceObjectAt(cellPos, database.itemPreviewData[selectedObjectIndex].size)) return;
        
        int index = _buildingPlacer.PlaceBuilding(database.itemPreviewData[selectedObjectIndex].prefab,
            cellPos + cellIndicator.cellOffset);

        gridData.AddObject(cellPos,
            database.itemPreviewData[selectedObjectIndex].size,
            database.itemPreviewData[selectedObjectIndex].id, 
            index);
    }

    public void UpdateState(Vector3Int cellPos)
    {
        cellIndicator.SetColor(gridData.CanPlaceObjectAt(cellPos, database.itemPreviewData[selectedObjectIndex].size)
            ? Color.green : Color.red);
    }
}

