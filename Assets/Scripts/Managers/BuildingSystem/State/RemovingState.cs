using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    private BuildingPlacer _buildingPlacer;    
    private GridData gridData;
    private Sprite defaultCell;
    private CellIndicator cellIndicator;
    public RemovingState(BuildingPlacer buildingPlacer, GridData gridData, CellIndicator cellIndicator)
    {
        this._buildingPlacer = buildingPlacer;
        this.gridData = gridData;
        this.cellIndicator = cellIndicator;
        this.cellIndicator.SetSprite();
    }
    public void EndState()
    {
        cellIndicator.SetSprite();
    }

    public void OnAction(Vector3Int cellPos)
    {
        if (!gridData.CanPlaceObjectAt(cellPos, Vector2Int.one))
        {
            gameObjectIndex = gridData.GetRepresentationIndex(cellPos);
            if (gameObjectIndex == -1)
                return;
            gridData.RemoveObjectAt(cellPos);
            _buildingPlacer.RemoveBuildingAt(gameObjectIndex);
        }
    }

    public void UpdateState(Vector3Int cellPos)
    { 
        cellIndicator.SetColor(gridData.CanPlaceObjectAt(cellPos, Vector2Int.one) ? Color.yellow : Color.red);
    }
}
