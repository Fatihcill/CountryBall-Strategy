using UnityEngine;

public class Soldier : Unit
{
    public Vector3 spawnPosition;
    [SerializeField]protected int damage;
    
    protected override void Awake()
    {
        base.Awake();
        health = 10;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }   
    public override void TakeDamage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            // Handle soldier's death
        }
    }
    
    public void Move(Vector3 destination)
    {
        // Implementation of movement functionality goes here
    }

    public virtual void Attack(Unit target)
    {
        // Implementation of attack functionality goes here
    }

}