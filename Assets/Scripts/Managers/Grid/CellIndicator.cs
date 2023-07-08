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

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
        public void SetSprite(Sprite sprite = null)
        {
            _spriteRenderer.color = Color.white;
            if (sprite == null)
            {
                cellOffset = _defaultCellOffset;
                _spriteRenderer.sprite = _defaultSprite;
                return;
            }
            _spriteRenderer.sprite = sprite;
        }

    }
