using System;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    int hp;
    [SerializeField]
    int maxHp;
    public delegate void OnActorDeath();
    public event OnActorDeath onDeath;
    // Start is called before the first frame update

    private void Start()
    {
        hp = maxHp;
    }
    public void Hurt(uint damage)
    {
        if (!IsDead())
        {
            hp -= Convert.ToInt32(damage);
            if (IsDead())
            {
                onDeath.Invoke();
            }
        }
    }
    public bool IsDead() { return hp <= 0; }
    public int GetHp() { return hp; }
}
