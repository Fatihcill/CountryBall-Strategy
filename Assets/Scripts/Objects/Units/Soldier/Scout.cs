using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Soldier
{
    protected override void Awake()
    {
        base.Awake();
        this.damage = 5;
    }



    // Other methods and properties unique to Cavalry...
}
