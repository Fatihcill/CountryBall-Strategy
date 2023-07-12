using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Soldier
{ 
    protected override void Awake()
    {
        base.Awake();
        this.damage = 10;
    }
}
