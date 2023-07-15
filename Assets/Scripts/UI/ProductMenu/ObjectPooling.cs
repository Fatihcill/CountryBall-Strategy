using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    private Transform _parent;
    private GameObject _prefab;
    public IObjectPool<GameObject> pool;
    
    private void Awake() {
        pool = new ObjectPool<GameObject>(
            CreateObject,
            OnGet,
            OnRelease,
            OnRemove,
            maxSize: 100
        );
    }
    
    private GameObject CreateObject()
    {
        return Instantiate(_prefab, _parent);
    }

    private void OnGet(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(true);
        poolObject.transform.position = transform.position; 
    }

    private void OnRelease(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    private void OnRemove(GameObject poolObject)
    {
        Destroy(poolObject.gameObject);
    }

    public GameObject Create(GameObject prefab, Transform parent = null)
    {
        _parent = parent == null ? this.transform : parent;
        _prefab = prefab;
        return pool.Get();
    }
}
