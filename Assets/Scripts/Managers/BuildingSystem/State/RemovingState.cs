using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int _gameObjectIndex = -1;
    private ObjectPlacer _objectPlacer;    
    private Sprite _defaultCell;
    private CellIndicator _cellIndicator;
    public RemovingState(ObjectPlacer objectPlacer, CellIndicator cellIndicator)
    {
        this._objectPlacer = objectPlacer;
        this._cellIndicator = cellIndicator;
        _cellIndicator.SetDefaultCell();
    }
    public void EndState()
    {
        _cellIndicator.SetDefaultCell();
    }

    public void OnAction(Vector2Int cellPos)
    {
        if (!Map.Instance.IsCellOccupied(cellPos, Vector2Int.one))
        {
            _gameObjectIndex =Map.Instance.gridData.GetRepresentationIndex(cellPos);
            if (_gameObjectIndex == -1)
                return;
            Map.Instance.gridData.RemoveObjectAt(cellPos);
            _objectPlacer.RemoveObjectAt(_gameObjectIndex);
        }
    }

    public void UpdateState(Vector2Int cellPos)
    { 
        _cellIndicator.UpdateState(Map.Instance.IsCellOccupied(cellPos) ? Color.yellow : Color.red);
    }
}
