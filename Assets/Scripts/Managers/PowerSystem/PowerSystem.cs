using System;

public class PowerSystem
{
    private int _power;
    public PowerSystem(int power)
    {
        _power = power;
    }
    
    public int GetPower()
    {
        return _power;
    }

    public void UpdatePower(int amount)
    {
        _power += amount;
        if (_power < 0) 
            _power = 0;
    }
}