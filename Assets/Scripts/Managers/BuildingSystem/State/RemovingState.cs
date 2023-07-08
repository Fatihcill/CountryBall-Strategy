using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int _gameObjectIndex = -1;
    private BuildingPlacer _buildingPlacer;    
    private GridData _gridData;
    private Sprite _defaultCell;
    private CellIndicator _cellIndicator;
    public RemovingState(BuildingPlacer buildingPlacer, GridData gridData, CellIndicator cellIndicator)
    {
        this._buildingPlacer = buildingPlacer;
        this._gridData = gridData;
        this._cellIndicator = cellIndicator;
        _cellIndicator.SetDefaultCell();
    }
    public void EndState()
    {
        _cellIndicator.SetDefaultCell();
    }

    public void OnAction(Vector3Int cellPos)
    {
        if (!_gridData.CanPlaceObjectAt(cellPos, Vector2Int.one))
        {
            _gameObjectIndex = _gridData.GetRepresentationIndex(cellPos);
            if (_gameObjectIndex == -1)
                return;
            _gridData.RemoveObjectAt(cellPos);
            _buildingPlacer.RemoveBuildingAt(_gameObjectIndex);
        }
    }

    public void UpdateState(Vector3Int cellPos)
    { 
        _cellIndicator.UpdateState(_gridData.CanPlaceObjectAt(cellPos, Vector2Int.one) ? Color.yellow : Color.red);
    }
}
