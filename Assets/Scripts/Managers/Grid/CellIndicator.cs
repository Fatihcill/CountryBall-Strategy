using System;
using UnityEngine;

public class CellIndicator : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    private Vector3 _defaultCellOffset;
    public Vector3 cellOffset;
    public Vector3Int cellPos;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        cellOffset = _defaultCellOffset = new Vector3(0.5f, 0.5f);
    }

    private void Update()
    {
        UpdateCellPosition(inputManager.GetSelectedMapPosition());
    }

    private void UpdateCellPosition(Vector2 mousePosition)
    {
        cellPos = Vector3Int.zero;
        cellPos.x = mousePosition.x >= 0 ? (int)(mousePosition.x) : (int)(mousePosition.x - 1);
        cellPos.y = mousePosition.y >= 0 ? (int)(mousePosition.y) : (int)(mousePosition.y - 1);
        
        transform.position = cellPos + cellOffset;
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
