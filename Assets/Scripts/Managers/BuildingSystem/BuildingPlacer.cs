using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<Building> placedBuildingObjects = new();
    [SerializeField] private ObjectsDatabaseManager databaseManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ObjectPooling objectPool;

    public int PlaceBuilding(GameObject prefab, Vector3 pos, int id, ObjectPreviewData.ObjectType type)
    {
        if (type != ObjectPreviewData.ObjectType.Building) return -1;
        Building newBuilding = objectPool.Create(prefab, parent).GetComponent<Building>();
        newBuilding.SetBuilding(id, databaseManager, inputManager, objectPool.pool);
        newBuilding.transform.position = pos;
        placedBuildingObjects.Add(newBuilding);
        return placedBuildingObjects.Count - 1;
    }
 
    public void RemoveBuildingAt(int gameObjectIndex)
    {
        if (placedBuildingObjects.Count <= gameObjectIndex) return;
        placedBuildingObjects[gameObjectIndex].Die();
        placedBuildingObjects[gameObjectIndex] = null;
    }
}

