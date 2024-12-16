using System;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Chase,
    Attack
}
[RequireComponent(typeof(TargetFinder))]
public abstract class Enemy : Entity
{
    public EnemyState state;
    protected delegate void StateAction();
    protected Dictionary<EnemyState, StateAction> enemyStates;
    [NonSerialized]
    public TargetFinder targetInfo;
    
    protected override void Awake()
    {
        targetInfo = GetComponent<TargetFinder>();
        enemyStates = new Dictionary<EnemyState, StateAction>();
        enemyStates.Add(EnemyState.Idle,IdleState);
        enemyStates.Add(EnemyState.Attack,AttackState);
        targetInfo.onTargetedFound += OnTargetFound;
        targetInfo.onTargetLost += OnTargetLost;
        base.Awake();
    }
    protected virtual void Update()
    {
        enemyStates[state]();
    }
    protected abstract void OnTargetFound();
    protected abstract void OnTargetLost();
    protected abstract void IdleState();
    protected abstract void AttackState();
}