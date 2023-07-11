using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : ObjectModel
{
    private float _speed;
    public Cell unitCell;   
    public List<Cell> pathVectorList;
    private int _currentPathIndex;
    private bool _changedPos;
    public abstract void TakeDamage(int amount);
    protected virtual void Awake()
    {
        _currentPathIndex = 0;
        _speed = 5;
        health = 10;
        unitCell = new Cell(0, 0);
    }
    protected virtual void Update()
    {
        HandleMovement();
    }
    
    protected void OnMouseDown()
    {
        unitCell.pos = Map.Instance.cellIndicator.GetCellWorldPos(transform.position);
        GameManager.Instance.inputManager.OnAction.AddListener(StartAction);
        GameManager.Instance.inputManager.OnExit.AddListener(StopAction);
    }

    public void SetTargetPosition() {
        _currentPathIndex = 0;
        pathVectorList = GameManager.Instance.pathfinding.FindPath(unitCell, Map.Instance.cellIndicator.currentCell);
    }

    private void HandleMovement() {
        if (pathVectorList != null && pathVectorList.Count > 0)
        {
            Vector3 targetPosition = pathVectorList[_currentPathIndex].worldPos;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * _speed * Time.deltaTime;
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
        Map.Instance.gridData.RemoveObjectAt(unitCell.pos);
        Map.Instance.gridData.AddObject(currentPos, ObjectData.size, ObjectData.id);
        unitCell.pos = currentPos;
    }
    protected void StartAction()
    {
        SetTargetPosition();
    }
    
    protected void StopAction()
    {
        GameManager.Instance.inputManager.OnAction.RemoveListener(StartAction);
        GameManager.Instance.inputManager.OnExit.RemoveListener(StopAction);
    }

}