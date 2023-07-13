using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : ObjectModel
{
    protected float Speed;
    private int _currentPathIndex;
    private bool _changedPos;
    public Cell unitCell;   
    private readonly Cell _target = new(0, 0);
    private List<Cell> _pathVectorList;
    protected ObjectModel TargetGameObject;

    private Vector3 _nextCellPos, _moveDir;
    protected virtual void Awake()
    {
        IsImmortal = false;
        _currentPathIndex = 0;
        Speed = 5;
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

    protected abstract void ActionToTarget();
    public void SetTargetPosition() 
    {
        _currentPathIndex = 0;
        _target.pos = Map.Instance.cellIndicator.currentCell.pos;
        int targetIndex = Map.Instance.gridData.GetRepresentationIndex(_target.pos);
        TargetGameObject = GameManager.Instance.placementSystem.objectPlacer.GetPlacedObject(targetIndex);
        _pathVectorList = GameManager.Instance.pathfinding.FindPath(unitCell, _target);
    }

    private void UpdatePos(Vector2Int currentPos)
    {
        Map.Instance.gridData.RemoveObjectAt(unitCell.pos);
        Map.Instance.gridData.AddObject(currentPos, ObjectData.size, ObjectData.id, placedObjectIndex);
        unitCell.pos = currentPos;
    }
    
    protected void StartAction()
    {
        SetTargetPosition();
        if (TargetGameObject != null)
            ActionToTarget();
        StopAction();
    }

    private void StopAction()
    {
        GameManager.Instance.inputManager.OnAction.RemoveListener(StartAction);
        GameManager.Instance.inputManager.OnExit.RemoveListener(StopAction);
    }
    
    private void HandleMovement() 
    {
        if (_pathVectorList is { Count: > 0 } && _currentPathIndex < _pathVectorList.Count)
        {
            _nextCellPos = _pathVectorList[_currentPathIndex].worldPos;
            MovementAction();
        }
    }

    private void MovementAction()
    {
        if (!Map.Instance.IsCellAvailable(_pathVectorList[_currentPathIndex].pos)) 
        {
            _currentPathIndex = 0;
            _pathVectorList = null;
            StopAction();
            return;
        }
        if (Vector3.Distance(transform.position, _nextCellPos) > 0.1f)
        {
            _moveDir = (_nextCellPos - transform.position).normalized;
            transform.position += _moveDir * (Speed * Time.deltaTime);
        } 
        else
        {
            _currentPathIndex++;
            UpdatePos(_pathVectorList[_currentPathIndex - 1].pos);
            if (_currentPathIndex >= _pathVectorList.Count) // Arrived the Target
            {
                _currentPathIndex = 0;
                _pathVectorList = null;
                StopAction();
            }
        }
    }
}