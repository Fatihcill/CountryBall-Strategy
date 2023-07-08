using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedBuildingObjects = new();
    [SerializeField] private Transform objectsParent;

    private void Awake()
    {
        if (objectsParent == null)
        {
            objectsParent = GameObject.Find("Buildings").transform;
        }
    }

    public int PlaceBuilding(GameObject prefab, Vector3 pos)
    {
        GameObject newObject = Instantiate(prefab, objectsParent);
        newObject.transform.position = pos;
        placedBuildingObjects.Add(newObject);
        return placedBuildingObjects.Count - 1;
    }

    public void RemoveBuildingAt(int gameObjectIndex)
    {
        if (placedBuildingObjects.Count <= gameObjectIndex) return;
        Destroy(placedBuildingObjects[gameObjectIndex]);
        placedBuildingObjects[gameObjectIndex] = null;
    }
}
