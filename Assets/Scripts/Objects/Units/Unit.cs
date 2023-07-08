using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int health;
    ObjectPreviewData _objectPreviewData;
    [SerializeField] InputManager inputManager;
    private GridData _gridData;
    private Vector3Int _oldPos;
    private CellIndicator _cellIndicator;
    public abstract void TakeDamage(int amount);
    protected virtual void Awake()
    {
        health = 10;
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        //check mouse clicked on the unit
        // inputManager.    OnClicked += PlaceStructure;
        // inputManager.OnExit += StopPlacement;
    }



    public void setItemPreview(ObjectPreviewData objectPreviewData)
    {
        _objectPreviewData = objectPreviewData;
    }
    public void Move(Vector3Int destination)
    {
        _gridData.RemoveObjectAt(_oldPos);
        _gridData.AddObject(destination, _objectPreviewData.size, _objectPreviewData.id, -1);
        transform.position = destination;
        _oldPos = destination;
    }


}