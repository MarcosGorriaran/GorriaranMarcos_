using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField]
    float rotationRange;
    protected override void AttackState()
    {
        weapon.Fire(transform.position, target.transform.position, gameObject);
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
