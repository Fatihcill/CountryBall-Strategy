using System;
using UnityEngine;
using UnityEngine.Tilemaps;

class Map : MonoBehaviour
{
    public GridData GridData;
    private Tilemap _environment;
    public CellIndicator cellIndicator;
    [SerializeField] InputManager inputManager;
    private void Awake()
    {
        _environment = transform.Find("Grass").GetComponent<Tilemap>();
        cellIndicator = GetComponentInChildren<CellIndicator>();
        GridData = new GridData(_environment);
    }
    private void OnMouseDown()
    {
        inputManager.OnExit?.Invoke();
    }
}