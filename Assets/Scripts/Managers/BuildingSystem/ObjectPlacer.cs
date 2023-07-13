using System.Collections.Generic;
using UnityEngine;


public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<ObjectModel> placedObjects = new();
    [SerializeField] private Transform pfHealthBar;
    
    public int PlaceObject(GameObject prefab, Vector3 pos, int id)
    {
        ObjectModel objectModel = Instantiate(prefab, parent).GetComponent<ObjectModel>();
        objectModel.SetObject(id, pfHealthBar, placedObjects.Count);
        objectModel.transform.position = pos;
        placedObjects.Add(objectModel);
        return placedObjects.Count - 1;
    }
 
    public void DestroyObjectAt(int gameObjectIndex)
    {
        if (placedObjects.Count <= gameObjectIndex) return;
        placedObjects[gameObjectIndex].DestroyObject();
        placedObjects[gameObjectIndex] = null;
    }

    public ObjectModel GetPlacedObject(int gameObjectIndex)
    {
        if (gameObjectIndex == -1)
            return null;
        return placedObjects[gameObjectIndex];
    }
}

