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
        meleeRange.onTargetLost += OnTargetFound;
        meleeRange.target = targetInfo.target;
    }
    void InsideMeleeRange()
    {
        state = EnemyState.Attack;
        StopAgent();
    }
    protected override void AttackState()
    {
        weapon.Fire(transform.position,targetInfo.target.transform.position, gameObject);
    }
}
