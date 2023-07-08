using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Building : MonoBehaviour
{
    protected int health;
    private InputManager _inputManager;
    private ObjectPreviewData _buildingData;
    private IObjectPool<GameObject> _objectPool;

    protected bool isProduce;
    private int _id;
    protected virtual void Awake()
    {
        isProduce = false;
    }
    protected virtual void OnMouseDown()
    {
        _inputManager.ShowInformationMenu(_buildingData.name, _buildingData.preview, "", isProduce);
    }
    public void SetPool(IObjectPool<GameObject> pool)
    {
        _objectPool = pool;
    }



    public void SetBuilding(int id, ObjectsDatabaseManager databaseManager, InputManager inputManager, IObjectPool<GameObject> pool)
    {
        this._id = id;
        this._inputManager = inputManager;
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