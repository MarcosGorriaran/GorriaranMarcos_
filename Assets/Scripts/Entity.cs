
using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(HPManager))]
public abstract class Entity : MonoBehaviour, IAttack, ITargetable
{
    const string xParameterName = "MovementX";
    const string yParameterName = "MovementY";
    protected Animator animator;
    protected HPManager lifeManager;
    [SerializeField]
    public Weapon weapon;
    [SerializeField]
    private Group groupMember;
    [SerializeField] 
    private AudioSource hitSound;
    [SerializeField]
    private float damageInfoDuration = 1f;
    private Color spriteDefColor;
    private Coroutine damageInfo;
    public Group GroupMember 
    {
        get { return groupMember; }
        set { groupMember = value; }
    }
    public float ThreatLevel { get; set; } = 0;

    void OnEnable()
    {
        lifeManager.Revive();
    }

    protected virtual void Awake()
    {
        lifeManager = GetComponent<HPManager>();
        animator = GetComponentInChildren<Animator>();
        lifeManager.onDeath += OnDeath;
        lifeManager.onRevive += OnRevive;
        lifeManager.onHPChange += OnHpChange;
        spriteDefColor = GetComponentInChildren<SpriteRenderer>().color;
    }
    private void OnHpChange(int hpValue)
    {
        if (hpValue < 0)
        {
            if (damageInfo != null)
            {
                StopCoroutine(damageInfo);
            }
            hitSound.Play();
            damageInfo = StartCoroutine(HPHarmedInfo());
        }
    }
    private IEnumerator HPHarmedInfo()
    {

        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(damageInfoDuration);
        sprite.color = spriteDefColor;
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
    protected void SetAnimationDirection(float xCord, float yCord)
    {
        animator.SetFloat(xParameterName,xCord);
        animator.SetFloat(yParameterName,yCord);
    }
}
