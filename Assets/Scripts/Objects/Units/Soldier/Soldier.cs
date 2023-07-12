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
    public virtual void Attack(ObjectModel target)
    {
        // Implementation of attack functionality goes here
    }
}
