using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<ObjectModel> placedObjects = new();
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ObjectPooling objectPool;

    public int PlaceObject(GameObject prefab, Vector3 pos, int id, ObjectPreviewData.ObjectType type)
    {
        ObjectModel objectModel = objectPool.Create(prefab, parent).GetComponent<ObjectModel>();
        objectModel.SetObject(id, objectPool.pool);
        objectModel.transform.position = pos;
        placedObjects.Add(objectModel);
        return placedObjects.Count - 1;
    }
 
    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedObjects.Count <= gameObjectIndex) return;
        placedObjects[gameObjectIndex].Die();
        placedObjects[gameObjectIndex] = null;
    }
}

