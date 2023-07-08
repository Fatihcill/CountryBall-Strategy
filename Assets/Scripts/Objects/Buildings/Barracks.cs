using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    protected override void Awake()
    {
        base.Awake();
        this.health = 100;
        this.isProduce = true;
    }
}
