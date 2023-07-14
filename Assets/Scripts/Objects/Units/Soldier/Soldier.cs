using System.Collections;
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
            StartCoroutine(AttackTarget());
            //TargetGameObject.TakeDamage(damage);
        }
    }
    
    IEnumerator AttackTarget()
    {
        while (true)
        {
            if (TargetGameObject != null)
            {
                if (!UnitMove.IsMoving)
                {
                    AnimManager.PlayOne(AnimationTypes.Attack);
                    TargetGameObject.TakeDamage(damage);
                }            
            }   
            else
            {
                AnimManager.PlayOne(AnimationTypes.Idle);
                StopCoroutine(AttackTarget());
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    
    public virtual void Attack(ObjectModel target)
    {
        target.TakeDamage(damage);
    }
}
