using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Transform _transform;

    private void Start()
    {
        _transform = transform.Find("Bar");
    }

    public void Setup(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        healthSystem.OnHealthChanged += healthSystemOnHealthChanged;
    }
    
    private void healthSystemOnHealthChanged(object sender, EventArgs e)
    {
        _transform.localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }
}