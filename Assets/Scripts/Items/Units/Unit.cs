using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int health;
    ItemPreviewData _itemPreviewData;
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
        _gridData = GameObject.Find("Map").GetComponent<Map>().gridData;
        _oldPos = new Vector3Int((int)transform.position.x, (int)transform.position.y);
        _gridData.AddObject( _oldPos,
            Vector2Int.one, 2, -1);

        _cellIndicator = GameObject.Find("Map").GetComponent<Map>().cellIndicator;
    }
    protected virtual void Update()
    {
        //check mouse clicked on the unit
        // inputManager.OnClicked += PlaceStructure;
        // inputManager.OnExit += StopPlacement;
    }

    protected virtual void OnMouseDown()
    {
        //inputManager.OnClicked += PlaceStructure;
    }

    public void setItemPreview(ItemPreviewData itemPreviewData)
    {
        _itemPreviewData = itemPreviewData;
    }
    public void Move(Vector3Int destination)
    {
        _gridData.RemoveObjectAt(_oldPos);
        _gridData.AddObject(destination, _itemPreviewData.size, _itemPreviewData.id, -1);
        transform.position = destination;
        _oldPos = destination;
    }


}