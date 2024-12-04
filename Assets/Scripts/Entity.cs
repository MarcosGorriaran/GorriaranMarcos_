
using System;
using UnityEngine;
[RequireComponent(typeof(HPManager))]
public abstract class Entity : MonoBehaviour
{
    public HPManager lifeManager;


    protected void Start()
    {
        lifeManager = GetComponent<HPManager>();
        lifeManager.onDeath += OnDeath;
    }
    protected abstract void Attack(Vector2 direction);
    protected abstract void OnDeath();
}
