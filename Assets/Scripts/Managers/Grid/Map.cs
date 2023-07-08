using UnityEngine;
using UnityEngine.Tilemaps;

class Map : MonoBehaviour
{
    public GridData gridData;
    public CellIndicator cellIndicator;
    private Tilemap _environment;
    private void Awake()
    {
        _environment = transform.Find("Grass").GetComponent<Tilemap>();
        cellIndicator = GetComponentInChildren<CellIndicator>();
        gridData = new GridData(_environment);
    }

    private void Update()
    {
    }
}