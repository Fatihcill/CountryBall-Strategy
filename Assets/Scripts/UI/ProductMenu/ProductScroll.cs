    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ProductScroll : MonoBehaviour
    {
        public RectTransform content;
        
        [SerializeField] List<GameObject> cloneItems = new();
        [SerializeField] GameObject [] items;
        [SerializeField] GameObject buildingButtonPrefab;
        [SerializeField] ObjectPooling objectPool;
        [SerializeField] UIManager uiManager;
        
        private GridLayoutGroup _contentGridLayout;
        private RectTransform _rectTransform;
        private ScrollRect _scrollRect;
        private int _numberOfLine;
        private float _width;
        
        private void Start()
        {
            if (!content)  
                content = transform.GetChild(0).GetComponent<RectTransform>();
            _scrollRect = GetComponent<ScrollRect>();
            _rectTransform = GetComponent<RectTransform>();
            _contentGridLayout = content.GetComponent<GridLayoutGroup>();
            uiManager = transform.parent.GetComponent<UIManager>();
            objectPool = GetComponent<ObjectPooling>();
            items = uiManager.InitializeButtons(ObjectPreviewData.ObjectType.Building, buildingButtonPrefab, content);
            _width = (_contentGridLayout.cellSize.y + _contentGridLayout.spacing.y) * items.Length;
            _numberOfLine = (int)Math.Ceiling(_rectTransform.rect.height / _width);
            for (int j = 0; j < (_numberOfLine * 2); j++)
            {
                InstantiateItem();
            }
        }
        
        private void InstantiateItem()
        {
            foreach (var t in items)
            {
                cloneItems.Add(objectPool.Create(t, content));
            }
        }
        
        private void MoveContent()
        {
            var position = content.position;
            position = new Vector3(position.x, position.y + _width * (_numberOfLine), position.z);
            content.position = position;
        }

        private void DestroyItems()
        {
            for (int i = cloneItems.Count - 1; i >= items.Length; i--)
            {
                objectPool.pool.Release(cloneItems[i]);
                cloneItems.RemoveAt(i);
            }
        }
        
        public void OnScroll()
        {
            if (_scrollRect.verticalNormalizedPosition >= 1f)
            {
                DestroyItems();
                MoveContent();
            }
            if (_scrollRect.verticalNormalizedPosition <= 0.1f)
            {
                InstantiateItem();
            }
        }
    }
