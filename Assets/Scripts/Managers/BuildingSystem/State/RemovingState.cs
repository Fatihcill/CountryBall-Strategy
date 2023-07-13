using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int _gameObjectIndex = -1;
    private readonly ObjectPlacer _objectPlacer;    
    private Sprite _defaultCell;
    private readonly CellIndicator _cellIndicator;
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
        if (!Map.Instance.IsCellAvailable(cellPos, Vector2Int.one))
        {
            _gameObjectIndex =Map.Instance.gridData.GetRepresentationIndex(cellPos);
            if (_gameObjectIndex == -1)
                return;
            Map.Instance.gridData.RemoveObjectAt(cellPos);
            _objectPlacer.DestroyObjectAt(_gameObjectIndex);
        }
    }

    public void UpdateState(Vector2Int cellPos)
    { 
        _cellIndicator.UpdatePlacementState(Map.Instance.IsCellAvailable(cellPos) ? Color.yellow : Color.red);
    }
}
