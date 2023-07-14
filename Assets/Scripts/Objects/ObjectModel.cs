using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ObjectModel : MonoBehaviour
{
    protected bool IsImmortal;
    protected int MaxHealth;
    public ObjectPreviewData objectData;
    private HealthSystem _healthSystem;
    public int placedObjectIndex;
    
    public void SetObject(int id, Transform pfHealthBar, int index)
    {
        if (!IsImmortal)
        {
            _healthSystem = new HealthSystem(MaxHealth);
            Transform newHealthBar = Instantiate(pfHealthBar, transform);
            HealthBar healthBar =  newHealthBar.GetComponent<HealthBar>();
            healthBar.Setup(_healthSystem);
        }
        placedObjectIndex = index;
        objectData = GameManager.Instance.database.GetObjectData(id);
        if (objectData == null)
            throw new Exception("Object can't access the database");
    }
    
    public void DestroyObject() 
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int amount)
    {
        if (IsImmortal) return;
        _healthSystem.Damage(amount);
        if (_healthSystem.GetHealth() <= 0)
        {
            Map.Instance.gridData.RemoveObject(placedObjectIndex);
            GameManager.Instance.placementSystem.objectPlacer.DestroyObjectAt(placedObjectIndex);
        }
    }
    
}