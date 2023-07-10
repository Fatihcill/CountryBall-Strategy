using UnityEngine;

public enum SoldierType
{
    Archer,
    Scout,
    Knight
}
public class Soldier : Unit
{
    public Vector3 spawnPosition;
    [SerializeField]protected int damage;
    
    protected override void Awake()
    {
        base.Awake();
        health = 10;
    }
    public override void TakeDamage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            // Handle soldier's death
            
        }
    }
    public virtual void Attack(Unit target)
    {
        // Implementation of attack functionality goes here
    }
}
