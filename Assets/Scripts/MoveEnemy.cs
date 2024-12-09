using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(NavMeshAgent))]
public abstract class MoveEnemy : Enemy, IMove
{
    NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        enemyStates.Add
        agent = GetComponent<NavMeshAgent>();
    }
    public void Move(Vector2 target)
    {
        agent.Move(target);
    }
    protected override void AttackState()
    {
        throw new System.NotImplementedException();
    }
    protected override void IdleState()
    {
        throw new System.NotImplementedException();
    }
    protected void ChaseState()
    {
        throw new System.NotImplementedException();
    }
    protected override void OnTargetFound()
    {
        state = EnemyState.Chase;
    }
}
