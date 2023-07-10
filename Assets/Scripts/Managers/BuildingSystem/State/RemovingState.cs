using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int _gameObjectIndex = -1;
    private BuildingPlacer _buildingPlacer;    
    private Sprite _defaultCell;
    private CellIndicator _cellIndicator;
    public RemovingState(BuildingPlacer buildingPlacer, CellIndicator cellIndicator)
    {
        this._buildingPlacer = buildingPlacer;
        this._cellIndicator = cellIndicator;
        _cellIndicator.SetDefaultCell();
    }
    public void EndState()
    {
        _cellIndicator.SetDefaultCell();
    }

    public void OnAction(Vector2Int cellPos)
    {
        if (!Map.instance.IsCellOccupied(cellPos, Vector2Int.one))
        {
            _gameObjectIndex =Map.instance.gridData.GetRepresentationIndex(cellPos);
            if (_gameObjectIndex == -1)
                return;
            Map.instance.gridData.RemoveObjectAt(cellPos);
            _buildingPlacer.RemoveBuildingAt(_gameObjectIndex);
        }
    }

    public void UpdateState(Vector2Int cellPos)
    { 
        _cellIndicator.UpdateState(Map.instance.IsCellOccupied(cellPos) ? Color.yellow : Color.red);
    }
}
