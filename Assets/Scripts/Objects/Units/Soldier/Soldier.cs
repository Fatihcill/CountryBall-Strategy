using System.Collections;
using UnityEngine;

public class Soldier : Unit
{
    [SerializeField]protected int damage;
    private Vector3 TargetCurrentPos;
    protected override void Awake()
    {
        base.Awake();
        MaxHealth = 10;
    }

    protected override void ActionToTarget()
    {
        if (TargetGameObject.placedObjectIndex != this.placedObjectIndex)
        {
            TargetCurrentPos = TargetGameObject.transform.position; 
            StartCoroutine(AttackTarget());
        }
    }
    
    IEnumerator AttackTarget()
    {
        while (true)
        {
            if (TargetGameObject != null && TargetCurrentPos == TargetGameObject.transform.position)
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
    
    public virtual void Attack(ObjectModel target)
    {
        target.TakeDamage(damage);
    }
}
