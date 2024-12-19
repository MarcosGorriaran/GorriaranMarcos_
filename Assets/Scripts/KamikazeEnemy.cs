using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MoveEnemy
{
    protected override void AttackState()
    {
        weapon.Fire(transform.position, target.transform.position,gameObject);
        lifeManager.Hurt(Convert.ToUInt32(lifeManager.GetHp()));
    }
    protected override void OnDeath()
    {
        AttackState();
        base.OnDeath();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Entity collidedEntity) && collidedEntity.GroupMember == Group.Player)
        {
            AttackState();
        }
    }
}
