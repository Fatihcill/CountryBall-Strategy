using UnityEngine;

public class ObjectsDatabaseManager : MonoBehaviour
{
    [SerializeField] private ObjectsDatabase database;
    
    public ObjectPreviewData GetObjectData(int id)
    {
        return database.itemPreviewData.Find(item => item.id == id);
    }
      
}