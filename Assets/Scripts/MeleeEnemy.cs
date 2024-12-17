using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MoveEnemy
{
    [SerializeField]
    TargetFinder meleeRange;
    protected override void Awake()
    {
        base.Awake();
        meleeRange.onTargetedFound += InsideMeleeRange;
        meleeRange.onTargetLost += OutsideMeleeRange;
    }
    void InsideMeleeRange(Collider2D foundTarget)
    {
        target = foundTarget.GetComponent<Entity>();
        state = EnemyState.Attack;
        StopAgent();
    }
    void OutsideMeleeRange()
    {
        state = EnemyState.Chase;
        ContinueAgent();
    }
    protected override void AttackState()
    {
        weapon.Fire(transform.position,target.transform.position, gameObject);
    }
}
