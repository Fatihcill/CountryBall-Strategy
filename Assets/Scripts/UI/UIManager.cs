using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject[] _items;
    
    public GameObject[] InitializeButtons(ObjectPreviewData.ObjectType type, GameObject prefab, Transform parent)
    {
        List<ObjectPreviewData> objects =  GameManager.Instance.database.GetObjectByType(type);
        int length = objects.Count;
        _items = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            _items[i] = Instantiate(prefab, parent);
            _items[i].GetComponent<ProductButton>().SetButton(objects[i].id,
                objects[i].preview, objects[i].name);   
        }
        return _items;
    }
}
