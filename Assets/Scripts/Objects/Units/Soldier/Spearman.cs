using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Soldier
{
    protected override void Awake()
    {
        base.Awake();
        this.damage = 2;
    }
    public override void Attack(Unit target)
    {
        // Attack implementation for Infantry
    }

    // Other methods and properties unique to Infantry...
}
