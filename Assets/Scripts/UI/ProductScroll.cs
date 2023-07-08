    using UnityEngine;
    using UnityEngine.Pool;
    using UnityEngine.UI;

    public class ProductScroll : MonoBehaviour
    {
        [SerializeField] GameObject [] items;
        public RectTransform content;
        private RectTransform _rectTransform;
        private ScrollRect _scrollRect;
        private int _numberOfLine;
        
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _rectTransform = GetComponent<RectTransform>();
        }
    }
