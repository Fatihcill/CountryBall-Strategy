using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Soldier
{
    //todo: check this, it might change as Awake!
    protected override void Awake()
    {
        base.Awake();
        this.damage = 10;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    
    public override void Attack(Unit target)
    {
        // Attack implementation for Infantry
    }
}
