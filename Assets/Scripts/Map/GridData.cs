using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridData
{
    private Dictionary<Vector2Int, PlacementData> _placedObjects = new();
    readonly Tilemap _floorTilemap;

    public GridData(Tilemap tilemap)
    {
        _floorTilemap = tilemap;
    }

    public void AddObject(Vector2Int cellPos, Vector2Int objectSize, int id, int placedObjectIndex)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(cellPos, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, id, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contains this cell position{pos}");
            _placedObjects[pos] = data;
        }
    }

    private List<Vector2Int> CalculatePositions(Vector2Int cellPos, Vector2Int objectSize)
    {
        List<Vector2Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(cellPos + new Vector2Int(x, y));
            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector2Int cellPos, Vector2Int objectSize)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(cellPos, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos) || _floorTilemap.GetTile((Vector3Int)pos) == null)
            {
                return false;
            }
        }
        return true;
    }

    public int GetRepresentationIndex(Vector2Int cellPos)
    {
        if (_placedObjects.ContainsKey(cellPos))
            return _placedObjects[cellPos].placedObjectIndex;
        return -1;
    }

    public void RemoveObjectAt(Vector2Int cellPos)
    {
        foreach (var pos in _placedObjects[cellPos].OccupiedCells)
        {
            _placedObjects.Remove(pos);
        }
    }

    public void RemoveObject(int placedObjectIndex)
    {
        foreach (var item in _placedObjects)
        {
            if (item.Value.placedObjectIndex == placedObjectIndex)
            {
                RemoveObjectAt(item.Key);
                return;
            }
        }
    }
}

[Serializable]
public class PlacementData
{
    public readonly List<Vector2Int> OccupiedCells;
    public int id { get; set; }
    public int placedObjectIndex { get; set; }

    public PlacementData(List<Vector2Int> occupiedCells, int id, int placedObjectIndex)
    {
        this.OccupiedCells = occupiedCells;
        this.id = id;
        this.placedObjectIndex = placedObjectIndex;
    }
}