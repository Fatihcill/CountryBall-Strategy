    using UnityEngine;
    using UnityEngine.Pool;
    using UnityEngine.UI;

    public class ProductScroll : MonoBehaviour
    {
        [SerializeField] GameObject [] items;
        private IObjectPool<ProductItem> pool;
        public RectTransform content;
        private RectTransform rectTransform;
        private ScrollRect scrollRect;
        private int numberOfLine;
        
        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            pool = new ObjectPool<ProductItem>(CreateScrollItem, OnGet, OnRelease, OnDelete);
            rectTransform = GetComponent<RectTransform>();
        }

        
        private void Start()
        {
            //per item is 60 height and they need top and bottom limit for the scroll view. Calculate this with their length 
            numberOfLine = (int)rectTransform.rect.height / 60;
            



            // if(content.rect.height < this.GetComponent<RectTransform>().rect.height)
            // {
            //     Debug.Log(content.rect.height);    
            //     Debug.Log(GetComponent<RectTransform>().rect.height);
            //     pool.Get();
            //     Debug.Log(content.rect.height);    
            //     Debug.Log(GetComponent<RectTransform>().rect.height);
            // }


        }
        
        void OnScroll(Vector2 pos)
        {
            if (pos.y < 0.1f)
            {
                //pool.Get();
            }


        }
        
        private void Update() {
           if (Input.GetKeyDown(KeyCode.Space))
            {
                pool.Get();
            }
        }

        
        private ProductItem CreateScrollItem()
        {
            //instantiate all items
            ProductItem scrollItem = Instantiate(items[0], content).GetComponent<ProductItem>();
            return scrollItem;
        }
        
        private void OnGet(ProductItem scrollItem)
        {
            scrollItem.gameObject.SetActive(true);
        }
        
        private void OnRelease(ProductItem scrollItem)
        {
            scrollItem.gameObject.SetActive(false);
        }
        
        private void OnDelete(ProductItem scrollItem)
        {
            Destroy(scrollItem.gameObject);
        }
    }
