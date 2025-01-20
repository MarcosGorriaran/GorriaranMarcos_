using System;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    int hp;
    [SerializeField]
    int maxHp;
    public delegate void OnAction();
    public delegate void OnHPChangeAction(int damage);
    public event OnAction onDeath;
    public event OnAction onRevive;
    public event OnHPChangeAction onHPChange;
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
            onHPChange?.Invoke(-Convert.ToInt32(damage));
            if (IsDead())
            {
                onDeath?.Invoke();
            }

        }
    }
    public void Heal(uint heal)
    {
        if (!IsDead())
        {
            int healAmount = Convert.ToInt32(heal);
            int estimatedHealResult = healAmount + hp;
            if (healAmount+hp > maxHp)
            {
                hp = maxHp;
                onHPChange?.Invoke(estimatedHealResult-maxHp);
            }
            else
            {
                hp = estimatedHealResult;
                onHPChange?.Invoke(estimatedHealResult);
            }
        }    
    }
    public void Revive()
    {
        onHPChange?.Invoke(maxHp-hp);
        hp = maxHp;
        onRevive?.Invoke();
    }
    public bool IsDead() { return hp <= 0; }
    public int GetHp() { return hp; }
    public int GetMaxHp() { return maxHp; }
}
