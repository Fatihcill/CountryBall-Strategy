using System;
using System.Collections;
using UnityEngine;

public class PowerPlant : Building
{
    private float _powerIncreaseInterval;
    private int _powerIncreaseAmount;
    
    protected override void Awake()
    {
        base.Awake();
        this.MaxHealth = 50;
        this.isProduce = false;
        _powerIncreaseInterval = 2;
        _powerIncreaseAmount = 5;
    }

    private void Start()
    {
        StartCoroutine(IncreasePower());
    }

    private IEnumerator IncreasePower()
    {
        while (true) // Run this Coroutine in an infinite loop.
        {
            GameManager.Instance.UpdatePower(_powerIncreaseAmount);
            yield return new WaitForSeconds(_powerIncreaseInterval);
        }
    }
}
