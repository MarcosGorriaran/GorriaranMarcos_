using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    NavMeshAgent agent;  

    protected void Move(Vector2 targetMove)
    {
        agent.Move(targetMove);
    }

    protected override void Attack(Vector2 direction)
    {

    }
    protected override void OnDeath()
    {

    }
}
