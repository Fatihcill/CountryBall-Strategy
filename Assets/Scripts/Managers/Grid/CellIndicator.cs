using System;
using System.Collections.Generic;
using UnityEngine;

public class CellIndicator : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    private Vector3 _defaultCellOffset;
    public Vector3 cellOffset;
    public Cell currentCell = new (0, 0);
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        cellOffset = _defaultCellOffset = new Vector3(0.5f, 0.5f);
        currentCell.pos = Vector2Int.zero;
    }

    private void Update()
    {
        UpdateCellPosition(inputManager.GetSelectedMapPosition());

        if (Input.GetMouseButtonDown(0))
        {
            inputManager.OnClicked.RemoveAllListeners();
            inputManager.OnExit.RemoveAllListeners();
        }
    }

    private void UpdateCellPosition(Vector2 mousePosition)
    {
        currentCell.pos.x = mousePosition.x >= 0 ? (int)(mousePosition.x) : (int)(mousePosition.x - 1);
        currentCell.pos.y = mousePosition.y >= 0 ? (int)(mousePosition.y) : (int)(mousePosition.y - 1);
        
        transform.position = (Vector3Int)currentCell.pos + cellOffset;
    }

    public void UpdateState(Color color)
    {
        _spriteRenderer.color = color;
    }
    public void StartPlacement(Sprite preview, Vector3 size)
    {
        _spriteRenderer.color = Color.white;
        _spriteRenderer.sprite = preview;
        cellOffset = size / 2;
    }

    public void SetDefaultCell()
    {
        _spriteRenderer.color = Color.white;
        _spriteRenderer.sprite = _defaultSprite;
        cellOffset = _defaultCellOffset;
    }

}
