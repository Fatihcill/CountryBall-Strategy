using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Soldier
{
    protected override void Awake()
    {
        base.Awake();
        this.Damage = 2;
    }
}
