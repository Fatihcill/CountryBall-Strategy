using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDatabaseManager : MonoBehaviour
{
    [SerializeField] private ObjectsDatabase database;
    public List<ObjectPreviewData> units;
    public List<ObjectPreviewData> buildings;

    private void Awake()
    {
        foreach (var item in database.itemPreviewData)
        {
            if (item.type == ObjectPreviewData.ObjectType.Unit)
                units.Add(item);
            else if (item.type == ObjectPreviewData.ObjectType.Building)
                buildings.Add(item);
            else
                throw new Exception("Unknown object type");
        }
    }
    public List<ObjectPreviewData> GetObjectByType(ObjectPreviewData.ObjectType type)
    {
        return type switch
        {
            ObjectPreviewData.ObjectType.Unit => units,
            ObjectPreviewData.ObjectType.Building => buildings,
            _ => throw new Exception("Unknown object type")
        };
    }
    public ObjectPreviewData GetObjectData(int id)
    {
        return database.itemPreviewData.Find(item => item.id == id);
    }
}