using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public List<Cell> FindPath(Cell source, Cell destination)
    {
        int i = 100;
        Cell current = null;
        List<Cell> openSet = new();
        HashSet<Cell> closedSet = new();
        openSet.Add(source);
        
        while(openSet.Count > 0 && --i > 0)
        {
            current = openSet[0];

            foreach (var node in openSet)
            {
                if (node.fCost < current.fCost ||
                    node.fCost == current.fCost && node.hCost < current.hCost)
                {
                    current = node;
                }
            }
            openSet.Remove(current);
            closedSet.Add(current);
            
            // end
            if(current.pos == destination.pos)
            {
                return GetPath(source, current);
            }
            // examine each neighbour
            foreach(Cell neighbourTile in GetNeighbourTiles(current))
            {   
                // tile is occupied

                if (Map.Instance.IsCellOccupied(neighbourTile.pos) == false || closedSet.Contains(neighbourTile))
                    continue;
                // get the neighbout tile having the least cost
                int movementCost = current.gCost + GetDistance(current, neighbourTile);
                if( movementCost < neighbourTile.gCost || !openSet.Contains(neighbourTile))
                {
                    Cell neighbour = neighbourTile;
                    neighbour.gCost = movementCost;
                    neighbour.hCost = GetDistance( neighbourTile, destination);
                    neighbour.hCost = GetDistance(neighbourTile, destination);
                    neighbour.parent = current;

                    if(!openSet.Contains(neighbourTile))
                    {
                        openSet.Add(neighbourTile);
                    }
                }
            }
        }
        return null;
    }
   private List<Cell> GetNeighbourTiles(Cell currentTile)
   {

       List<Cell> neighbours = new List<Cell>();

       for( int i = -1; i <= 1; i++)
       {
           for( int j = -1; j <= 1; j++)
           {   
               //self
               if( i == 0 && j == 0)
               {
                   continue;
               }
               int x  = i + currentTile.pos.x;
               int y = j + currentTile.pos.y;
               neighbours.Add(Map.Instance.GetNode(x, y));
           }
       }
       return neighbours;
   }
       
   private List<Cell> GetPath(Cell sourceTile, Cell destinationTile)
   {
       List<Cell> path = new();
       Cell current = destinationTile;

       while( current.pos != sourceTile.pos)
       {
           path.Add(current);
           current = current.parent;
       }
       path.Reverse();
       return path;
   }
   
   private int GetDistance(Cell currentTile, Cell nextTile)
   {   
       int xDistance = Mathf.Abs(currentTile.pos.x - nextTile.pos.x);
       int yDistance = Mathf.Abs(currentTile.pos.y - nextTile.pos.y);

       if( xDistance > yDistance )
       {
           return 14 * yDistance + 10 * (xDistance - yDistance);
       }
       return 14 * xDistance + 10 * ( yDistance - xDistance);
   }
}