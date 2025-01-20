using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(NavMeshAgent))]
public abstract class MoveEnemy : Enemy, IMove
{
    const string MovingParameterName = "Walking";
    NavMeshAgent agent;
    

    protected override void Awake()
    {
        base.Awake();
        enemyStates.Add(EnemyState.Chase,ChaseState);
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }
    public void Move(Vector2 target)
    {
        agent.SetDestination(target);
        Vector2 direction = agent.desiredVelocity.normalized;
        SetAnimationDirection(direction.x, direction.y);
    }
    protected override void IdleState()
    {
        animator.SetBool(MovingParameterName, false);
    }
    protected void ChaseState()
    {
        animator.SetBool(MovingParameterName, true);
        Move(target.transform.position);
    }
    
    protected override void OnTargetFound(Collider2D foundTarget)
    {
        base.OnTargetFound(foundTarget);
        state = EnemyState.Chase;
    }
    protected override void OnTargetLost()
    {
        state = EnemyState.Idle;
        target = null;
    }
    protected void StopAgent()
    {
        agent.ResetPath();
        agent.isStopped = true;
        
    }
    protected void ContinueAgent()
    {
        agent.isStopped = false;
    }
}
