using UnityEngine;

public class Soldier : Unit
{
    [SerializeField]protected int damage;

    protected override void Awake()
    {
        base.Awake();
        MaxHealth = 10;
    }

    protected override void ActionToTarget()
    {
        if (TargetGameObject.placedObjectIndex != this.placedObjectIndex)
        {
            TargetGameObject.TakeDamage(damage);
        }
    }

    public virtual void Attack(ObjectModel target)
    {
        target.TakeDamage(damage);
    }
}
