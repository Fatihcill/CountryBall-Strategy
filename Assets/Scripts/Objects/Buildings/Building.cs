using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Building : MonoBehaviour
{
    protected int health;
    private ObjectPreviewData _buildingData;
    private IObjectPool<GameObject> _objectPool;
    [SerializeField]protected bool IsProduce, IsClicked;
    
    protected virtual void Awake()
    {
        IsClicked = false;
        IsProduce = false;
    }

    protected virtual void OnMouseUp()
    {
        OnInfo();
        GameManager.Instance.inputManager.OnHideInfo.AddListener(OnHide);
    }

    protected virtual void OnInfo()
    {
        GameManager.Instance.inputManager.ShowInformationMenu(
            _buildingData.name, _buildingData.preview, "", true);
        IsClicked = true;
    }

    protected virtual void OnHide()
    {
        Debug.Log("aaaa");
        GameManager.Instance.inputManager.HideInfo();
        IsClicked = false;
    }
    public void SetBuilding(int id, ObjectsDatabaseManager databaseManager, IObjectPool<GameObject> pool)
    {
        _buildingData = databaseManager.GetObjectData(id);
        _objectPool = pool;
        if (_buildingData == null)
            throw new Exception("Building can't access the database");
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
        _objectPool.Release(this.gameObject);
    }
    
}