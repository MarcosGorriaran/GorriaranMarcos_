using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField]
    float rotationRange;
    protected override void AttackState()
    {
        throw new System.NotImplementedException();
    }

    protected override void IdleState()
    {
        
    }

    protected override void OnTargetFound(Collider2D foundTarget)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTargetLost()
    {
        throw new System.NotImplementedException();
    }
}
