using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private int _health;
    private readonly int _healthMax;
    
    public HealthSystem(int healthMax)
    {
        _healthMax = healthMax;
        _health = healthMax;
    }
    
    public int GetHealth()
    {
        return _health;
    }
    
    public float GetHealthPercent()
    {
        return (float)_health / _healthMax;
    }
    
    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health < 0) 
            _health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
    
    public void Heal(int healAmount)
    {
        _health += healAmount;
        if (_health > _healthMax)
            _health = _healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
