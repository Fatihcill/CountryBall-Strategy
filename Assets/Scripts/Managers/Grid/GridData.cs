using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridData
{
    private Dictionary<Vector3Int, PlacementData> _placedObjects = new();
    Tilemap _floorTilemap;

    public GridData(Tilemap tilemap)
    {
        _floorTilemap = tilemap;
    }
    public void AddObject(Vector3Int cellPos, Vector2Int objectSize, int id, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(cellPos, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, id, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contains this cell position{pos}");
            _placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int cellPos, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(cellPos + new Vector3Int(x, y));
            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int cellPos, Vector2Int objectSize)
    {
        //if outside of map return false
        
        List<Vector3Int> positionToOccupy = CalculatePositions(cellPos, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos) || _floorTilemap.GetTile(pos) == null)
            {
                return false;
            }
        }
        return true;
    }

    public int GetRepresentationIndex(Vector3Int cellPos)
    {
        if (_placedObjects.ContainsKey(cellPos))
            return _placedObjects[cellPos].placedObjectIndex;
        return -1;
    }

    public void RemoveObjectAt(Vector3Int cellPos)
    {
        foreach (var pos in _placedObjects[cellPos].OccupiedCells)
        {
            _placedObjects.Remove(pos);
        }
    }
}


public class PlacementData
{
    public List<Vector3Int> OccupiedCells;
    public int id { get; set; }
    public int placedObjectIndex { get; set; }
    public PlacementData(List<Vector3Int> occupiedCells, int id, int placedObjectIndex)
    {
        this.OccupiedCells = occupiedCells;
        this.id = id;
        this.placedObjectIndex = placedObjectIndex;
    }
}
