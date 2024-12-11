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
        meleeRange.onTargetLost += OnTargetLost;
    }
    public override void Attack(Vector2 direction)
    {

    }
    void InsideMeleeRange()
    {
        state = EnemyState.Attack;
    }

    protected override void AttackState()
    {
        weapon.Fire(transform.position);
    }
}
