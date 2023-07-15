using System.Collections;
using UnityEngine;

public class Soldier : Unit
{
    protected int Damage;
    private Vector3 _targetCurrentPos;
    
    protected override void Awake()
    {
        base.Awake();
        MaxHealth = 10;
    }

    protected override void ActionToTarget()
    {
        if (TargetGameObject.placedObjectIndex != this.placedObjectIndex)
        {
            _targetCurrentPos = TargetGameObject.transform.position; 
            StartCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        while (true)
        {
            if (TargetGameObject != null && Vector3.Distance(_targetCurrentPos, TargetGameObject.transform.position) < 2f)
            {
                if (!UnitMove.IsMoving)
                {
                    AnimManager.PlayOne(AnimationTypes.Attack);
                    Attack(TargetGameObject);
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

    protected virtual void Attack(ObjectModel target)
    {
        target.TakeDamage(Damage);
    }
}
