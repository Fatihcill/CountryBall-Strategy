using System;
using UnityEngine;

[Serializable]
public class Cell
{
    public Vector2Int pos;
    public Vector2 worldPos;
    public int gCost, hCost;
    public Cell parent;
    
    public int fCost => gCost + hCost;

    public Cell(int x, int y)
    {
        pos.x = x;
        pos.y = y;
        worldPos = pos + (Vector2.one * 0.5f);
    }
}