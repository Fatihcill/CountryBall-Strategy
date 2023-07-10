using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    Transform spawnLocation;
    protected override void Awake()
    {
        base.Awake();
        spawnLocation = transform.GetChild(0);
        this.health = 100;
        this.IsProduce = true;
        spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    
    public void ProduceSoldier(int soldierType)
    {
        // TODO: Implement soldier production logic based on the soldierType
    }
    protected override void OnInfo()
    {
        base.OnInfo();
        spawnLocation.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void OnHide()
    {
        base.OnHide();
        spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
