using System;
using System.Collections.Generic;
using UnityEngine;

public class CellIndicator : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    private Vector2 _defaultCellOffset;
    public Vector2 cellOffset;
    public Cell currentCell = new (0, 0);
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        cellOffset = _defaultCellOffset = new Vector2(0.5f, 0.5f);
        currentCell.pos = Vector2Int.zero;
    }

    private void Update()
    {
        currentCell.pos = Vector2Int.FloorToInt(inputManager.GetSelectedMapPosition());
        transform.position =  currentCell.pos + cellOffset;
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
