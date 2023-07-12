using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : ObjectModel
{
    protected float Speed;
    public Cell unitCell;   
    public List<Cell> pathVectorList;
    private int _currentPathIndex;
    private bool _changedPos;
    
    protected virtual void Awake()
    {
        _currentPathIndex = 0;
        Speed = 5;
        health = 10;
        unitCell = new Cell(0, 0);
    }
    
    protected virtual void Update()
    {
        HandleMovement();
    }
    
    protected void OnMouseDown()
    {
        unitCell.pos = Vector2Int.FloorToInt(transform.position);
        GameManager.Instance.inputManager.OnAction.AddListener(StartAction);
        GameManager.Instance.inputManager.OnExit.AddListener(StopAction);
    }

    private Cell target = new(0, 0);
    public void SetTargetPosition() {
        _currentPathIndex = 0;
        target.pos = Map.Instance.cellIndicator.currentCell.pos;
        pathVectorList = GameManager.Instance.pathfinding.FindPath(unitCell, target);
    }

    private void HandleMovement() {
        if (pathVectorList != null && pathVectorList.Count > 0)
        {
            Vector3 targetPosition = pathVectorList[_currentPathIndex].worldPos;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * Speed * Time.deltaTime;
            } else {
                _currentPathIndex++;
                UpdatePos(pathVectorList[_currentPathIndex - 1].pos);
                if (_currentPathIndex >= pathVectorList.Count) {
                    pathVectorList = null;
                    StopAction();
                }
            }
        }
    }

    private void UpdatePos(Vector2Int currentPos)
    {
        int placedObjectIndex = Map.Instance.gridData.GetRepresentationIndex(unitCell.pos);
        Map.Instance.gridData.RemoveObjectAt(unitCell.pos);
        Map.Instance.gridData.AddObject(currentPos, ObjectData.size, ObjectData.id, placedObjectIndex);
        unitCell.pos = currentPos;
    }
    
    protected void StartAction()
    {
        SetTargetPosition();
        StopAction();
    }
    
    protected void StopAction()
    {
        GameManager.Instance.inputManager.OnAction.RemoveListener(StartAction);
        GameManager.Instance.inputManager.OnExit.RemoveListener(StopAction);
    }
}