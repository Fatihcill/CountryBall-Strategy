using System;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    private Transform _spawnLocation;
    private Vector2 _spawnOffset;
    private List<Vector2Int> myNeighbours;
    private bool _changedSpawnPos;
    protected override void Awake()
    {
        base.Awake();
        _spawnLocation = transform.GetChild(0);
        this.MaxHealth = 100;
        this.isProduce = true;
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
        _spawnOffset = Vector2.one * 0.5f;
    }

    private void Start()
    {
        myNeighbours = Map.Instance.gridData.GetNeighboursOfPlacedObject(Vector2Int.FloorToInt(transform.position));
    }

    public override void ProduceUnit(int soldierType)
    {
        base.ProduceUnit(soldierType);
        ObjectPreviewData unitData = GameManager.Instance.database.GetObjectData(soldierType);
        Vector2Int unitPos = Vector2Int.FloorToInt(_spawnLocation.transform.position);
        GameManager.Instance.placementSystem.StartCreatingObject(soldierType, unitPos);
        spawnPoint();
    }

    private void MoveSpawnPointManually()
    {
        _changedSpawnPos = true;
        _spawnLocation.transform.position = (Vector2)Map.Instance.currentPos + _spawnOffset;
    }

    protected override void OnInfo()
    {
        base.OnInfo();
        InputManager.Instance.OnAction.RemoveAllListeners();
        InputManager.Instance.OnAction.AddListener(MoveSpawnPointManually);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.white;
        spawnPoint();
    }

    private void spawnPoint()
    {
        if (!_changedSpawnPos)
        {
            foreach (Vector2Int neighbour in myNeighbours)
            {
                if (Map.Instance.IsCellAvailable(neighbour))
                {
                    _spawnLocation.transform.position = (Vector2)neighbour + _spawnOffset;
                    break;
                }
            }
        }
    }

    protected override void OnHide()
    {
        base.OnHide();
        InputManager.Instance.OnAction.RemoveListener(MoveSpawnPointManually);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
