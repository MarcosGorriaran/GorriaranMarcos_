using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(NavMeshAgent))]
public abstract class MoveEnemy : Enemy, IMove
{
    NavMeshAgent agent;
    

    protected override void Awake()
    {
        base.Awake();
        enemyStates.Add(EnemyState.Chase,ChaseState);
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }
    protected virtual void Update()
    {
        enemyStates[state]();
    }
    public void Move(Vector2 target)
    {
        agent.SetDestination(target);
    }
    protected override void IdleState()
    {
        
    }
    protected void ChaseState()
    {
        Move(targetInfo.target.transform.position);
    }
    
    protected override void OnTargetFound()
    {
        
        state = EnemyState.Chase;
    }
    protected override void OnTargetLost()
    {
        state = EnemyState.Idle;
    }
}
