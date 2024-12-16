
using System;
using UnityEngine;
[RequireComponent(typeof(HPManager))]
public abstract class Entity : MonoBehaviour, IAttack
{
    protected HPManager lifeManager;
    [SerializeField]
    protected Weapon weapon;
    void OnEnable()
    {
        lifeManager.Revive();
    }

    protected virtual void Awake()
    {
        lifeManager = GetComponent<HPManager>();
        lifeManager.onDeath += OnDeath;
        lifeManager.onRevive += OnRevive;
    }
    public virtual void Attack(Vector2 direction)
    {
        
    }
    protected virtual void OnDeath()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnRevive()
    {

    }
}
