using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private readonly ObjectPreviewData _selectedObject;
    private int _id;
    private readonly ObjectPlacer _objectPlacer;    
    private readonly CellIndicator _cellIndicator;
    
    public PlacementState(int id, ObjectPlacer objectPlacer, CellIndicator cellIndicator)
    {
        this._id = id;
        this._objectPlacer = objectPlacer;
        this._cellIndicator = cellIndicator;
        _selectedObject = GameManager.Instance.database.GetObjectData(id);
        if (_selectedObject != null)
        {
            cellIndicator.StartPlacement(_selectedObject.preview,
                (Vector2)_selectedObject.size);
        }
        else
            throw new System.Exception("No object with id " + id + " found");
    }

    public void EndState()
    {
        _cellIndicator.SetDefaultCell();
    }

    public void OnAction(Vector2Int cellPos)
    {
        if (!Map.Instance.IsCellOccupied(cellPos, _selectedObject.size)) return;
        
        int index = _objectPlacer.PlaceObject(_selectedObject.prefab,
            cellPos + _cellIndicator.cellOffset, _id, _selectedObject.type);
        if (index == -1) return;
        Map.Instance.gridData.AddObject(cellPos,
            _selectedObject.size,
            _selectedObject.id, 
            index);
    }

    public void UpdateState(Vector2Int cellPos)
    {
        _cellIndicator.UpdateState(Map.Instance.IsCellOccupied(cellPos, _selectedObject.size)
            ? Color.green : Color.red);
    }
}

