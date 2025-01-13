using System;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    int hp;
    [SerializeField]
    int maxHp;
    public delegate void OnAction();
    public event OnAction onDeath;
    public event OnAction onRevive;
    public event OnAction onHPChange;
    // Start is called before the first frame update

    private void Awake()
    {
        hp = maxHp;
    }
    public void Hurt(uint damage)
    {
        if (!IsDead())
        {
            hp -= Convert.ToInt32(damage);
            onHPChange?.Invoke();
            if (IsDead())
            {
                onDeath?.Invoke();
            }

        }
    }
    public void Revive()
    {
        hp = maxHp;
        onHPChange?.Invoke();
        onRevive?.Invoke();
    }
    public bool IsDead() { return hp <= 0; }
    public int GetHp() { return hp; }
    public int GetMaxHp() { return maxHp; }
}
