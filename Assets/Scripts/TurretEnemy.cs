using System;
using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField]
    float rotationRange;
    protected override void AttackState()
    {
        weapon.Fire(transform.position, target.transform.position, gameObject);

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.localRotation = Quaternion.Euler(0, 0, angle * 180f / Mathf.PI - 90f);
        
    }

    protected override void IdleState()
    {
        
    }

    protected override void OnTargetFound(Collider2D foundTarget)
    {
        base.OnTargetFound(foundTarget);
        state = EnemyState.Attack;
    }

    protected override void OnTargetLost()
    {
        state = EnemyState.Idle;
    }
}
