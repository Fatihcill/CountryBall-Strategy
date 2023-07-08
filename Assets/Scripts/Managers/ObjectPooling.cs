using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ObjectPooling : MonoBehaviour
{
    private GameObject _prefab;
    private Transform _parent; 
    public IObjectPool<GameObject> pool;
    [SerializeField] Transform defaultParent;
    private void Awake() {
        pool = new ObjectPool<GameObject>(
            CreateObject,
            OnGet,
            OnRelease,
            OnDie,
            maxSize: 100
        );
        _parent = defaultParent;
    }
    private GameObject CreateObject()
    {
        return Instantiate(_prefab, _parent);
    }

    private void OnGet(GameObject building)
    {
        building.gameObject.SetActive(true);
        building.transform.position = transform.position; 
    }

    private void OnRelease(GameObject building)
    {
        building.gameObject.SetActive(false);
    }

    private void OnDie(GameObject building)
    {
        Destroy(building.gameObject);
    }

    public GameObject Create(GameObject prefab, Transform parent = null)
    {
        Debug.Log(pool);
        _parent = parent == null ? defaultParent : parent;
        _prefab = prefab;
        return pool.Get();
    }
}
