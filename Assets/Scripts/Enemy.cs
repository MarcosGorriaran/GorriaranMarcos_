using System;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Chase,
    Attack
}
public abstract class Enemy : Entity
{
    [SerializeField]
    uint scorePoints;
    protected Entity target;
    public EnemyState state;
    protected delegate void StateAction();
    protected Dictionary<EnemyState, StateAction> enemyStates;
    [SerializeField]
    public TargetFinder targetFinder;
    
    protected override void Awake()
    {
        enemyStates = new Dictionary<EnemyState, StateAction>();
        enemyStates.Add(EnemyState.Idle,IdleState);
        enemyStates.Add(EnemyState.Attack,AttackState);
        targetFinder.onTargetedFound += OnTargetFound;
        targetFinder.onTargetLost += OnTargetLost;
        base.Awake();
    }
    protected virtual void Update()
    {
        enemyStates[state]();
    }
    protected virtual void OnTargetFound(Collider2D foundTarget)
    {
        target = foundTarget.GetComponent<Entity>();
    }
    protected override void OnDeath()
    {
        Player.instance.GivePoints(scorePoints);
        base.OnDeath();
    }
    protected abstract void OnTargetLost();
    protected abstract void IdleState();
    protected abstract void AttackState();
}