using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    public int health;
    private float _speed;
    private Cell _unityCell;   
    public List<Cell> pathVectorList;
    private int currentPathIndex;

    public abstract void TakeDamage(int amount);
    protected virtual void Awake()
    {
        currentPathIndex = 0;
        _speed = 5;
        health = 10;
        _unityCell = new Cell(0, 0);
    }

    protected virtual void Update()
    {
        HandleMovement();
    }
    
    private void UpdateCellPosition(Vector2 mousePosition)
    {
        _unityCell.pos.x = mousePosition.x >= 0 ? (int)(mousePosition.x) : (int)(mousePosition.x - 1);
        _unityCell.pos.y = mousePosition.y >= 0 ? (int)(mousePosition.y) : (int)(mousePosition.y - 1);
    }
    protected void OnMouseDown()
    {
        UpdateCellPosition(this.transform.position);
        GameManager.Instance.inputManager.OnAction.AddListener(StartAction);
        GameManager.Instance.inputManager.OnExit.AddListener(StopAction);
    }

    public void SetTargetPosition() {
        currentPathIndex = 0;
        pathVectorList = GameManager.Instance.pathfinding.FindPath(_unityCell, Map.instance.cellIndicator.currentCell);
    }

    private void HandleMovement() {
        if (pathVectorList != null && pathVectorList.Count > 0) 
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex].worldPos;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * _speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    pathVectorList = null;
                    StopAction();
                }
            }
        }
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