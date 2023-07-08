using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabase : ScriptableObject
{
    public List<ObjectPreviewData> itemPreviewData;
}


[Serializable]
public class ObjectPreviewData
{
    public enum ObjectType
    {
        Building,
        Unit
    }
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Vector2Int size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public Sprite preview { get; private set; }
    [field: SerializeField] public ObjectType type { get; private set; }
}