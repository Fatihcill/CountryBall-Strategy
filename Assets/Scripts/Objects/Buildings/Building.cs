using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class Building : MonoBehaviour
{
    protected int health;
    private ObjectPreviewData _buildingData;
    protected IObjectPool<GameObject> ObjectPool;
    [SerializeField]protected bool isProduce, isClicked;

    protected virtual void Awake()
    {
        isClicked = false;
       // SetBuilding(0, GameManager.Instance.obje);
    }
    public void SetBuilding(int id, IObjectPool<GameObject> pool)
    {
        _buildingData = GameManager.Instance.database.GetObjectData(id);
        ObjectPool = pool;
        if (_buildingData == null)
            throw new Exception("Building can't access the database");
    }
    protected virtual void OnMouseUp()
    {
        OnInfo();
        GameManager.Instance.inputManager.OnHideInfo.AddListener(OnHide);
    }

    protected virtual void OnInfo()
    {
        GameManager.Instance.inputManager.ShowInformationMenu(
            _buildingData.name, _buildingData.preview, isProduce, this);
        isClicked = true;
    }

    protected virtual void OnHide()
    {
        GameManager.Instance.inputManager.HideInfo();
        isClicked = false;
    }
    
    
    public virtual void ProduceSoldier(int soldierType)
    {
        if (!isProduce) return;
    }
    public void TakeDamage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            Die();
        }
    }
    public void Die() {
        ObjectPool.Release(this.gameObject);
    }
    
}