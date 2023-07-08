using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemsDatabase : ScriptableObject
{
    public List<ItemPreviewData> itemPreviewData;
}


[Serializable]
public class ItemPreviewData
{
    public enum ItemType
    {
        Building,
        Unit
    }
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Vector2Int size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public Sprite preview { get; private set; }
    
    //create a type enum, Unit or Building
    [field: SerializeField] public ItemType type { get; private set; }
}