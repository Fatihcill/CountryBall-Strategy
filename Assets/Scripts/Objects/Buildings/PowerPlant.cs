using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{
    protected override void Awake()
    {
        base.Awake();
        this.health = 50;
        this.IsProduce = false;
    }
    
    protected override void OnInfo()
    {
        base.OnInfo();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }
}
