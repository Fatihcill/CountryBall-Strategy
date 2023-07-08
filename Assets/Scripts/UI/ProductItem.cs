using UnityEngine;
using UnityEngine.Pool;

public class ProductItem : MonoBehaviour
{
    private IObjectPool<ProductItem> _itemPool;
    public int topLimit, bottomLimit; 

}