using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        healthSystem.OnHealthChanged += healthSystemOnHealthChanged;
    }
    
    private void healthSystemOnHealthChanged(object sender, EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }
}