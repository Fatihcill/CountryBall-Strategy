using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    private Transform _spawnLocation;
    private ObjectPreviewData.ObjectType _productType;

    protected override void Awake()
    {
        base.Awake();
        _spawnLocation = transform.GetChild(0);
        this.health = 100;
        this.isProduce = true;
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    
    public override void ProduceSoldier(int soldierType)
    {
        base.ProduceSoldier(soldierType);
        //Implement soldier production logic based on the soldierType
        //For now, we just spawn a soldier at the spawn location
        ObjectPreviewData soldier = GameManager.Instance.database.GetObjectData(soldierType);
        GameObject Unit = Instantiate(soldier.prefab);
        Unit.transform.position = _spawnLocation.transform.position;
        //ObjectPool.Get(soldier.prefab);
    }
    protected override void OnInfo()
    {
        base.OnInfo();
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void OnHide()
    {
        base.OnHide();
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
