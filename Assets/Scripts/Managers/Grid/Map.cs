using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public GridData gridData;
    public Dictionary<Vector2Int, Cell> cellValues = new();
    private Tilemap _floor;
    public CellIndicator cellIndicator;
    public Vector2Int currentPos;
    private void Awake()
    {
        Instance = this;
        _floor = transform.Find("Floor").GetComponent<Tilemap>();
        cellIndicator = GetComponentInChildren<CellIndicator>();
        gridData = new GridData(_floor);
    }

    private void Start()
    {
        cellIndicator.currentCell.pos = Vector2Int.zero;
    }

    public Cell GetNode(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);

        if (cellValues.TryGetValue(gridPos, out var node))
            return node;

        Cell newCell = new Cell(x, y);
        cellValues[newCell.pos] = newCell;
        return newCell;
    }

    public bool IsCellOccupied(Vector2Int cellPos, Vector2Int size = default)
    {
        if (size == default)
            size = Vector2Int.one;
        return gridData.CanPlaceObjectAt(cellPos, size);
    }
    private void Update()
    {
        currentPos = cellIndicator.currentCell.pos;
    }
}