using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janissary : Soldier
{
    protected override void Awake()
    {
        base.Awake();
        this.damage = 5;
    }
}
