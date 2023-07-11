using System;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectModel : MonoBehaviour
{
    public int health;
    protected ObjectPreviewData ObjectData;
    protected IObjectPool<GameObject> ObjectPool;
    
    public void SetObject(int id, IObjectPool<GameObject> pool)
    {
        ObjectData = GameManager.Instance.database.GetObjectData(id);
        ObjectPool = pool;
        if (ObjectData == null)
            throw new Exception("Object can't access the database");
    }
    
    public void Die() {
        ObjectPool.Release(this.gameObject);
    }
}