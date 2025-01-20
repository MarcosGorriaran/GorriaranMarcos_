using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;


public class Proyectile : MonoBehaviour
{
    const string LengthName = "length";
    Vector2 dir = new Vector2(0,0);
    GameObject owner;
    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime;
    [SerializeField]
    uint damage;
    [SerializeField]
    bool destroyOnContact = true;
    [SerializeField]
    bool keepEffects;
    ParentConstraint constraint;
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        constraint = GetComponent<ParentConstraint>();
        
    }
    private void OnEnable()
    {
        StartCoroutine(DeathCount());
        Animator[] animationControllers = GetComponentsInChildren<Animator>();
        if (animationControllers.Length > 0)
        {
            animationControllers.FirstOrDefault().SetFloat(LengthName, 1 / lifeTime);
        }
        ActivateObject();
        
    }
    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        
    }
    public void SetTrackTarget(Transform target)
    {
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = target.transform;
        constraintSource.weight = constraint.weight;
        if (constraint.sourceCount <= 0)
        {
            constraint.AddSource(constraintSource);
        }
        else
        {
            constraint.SetSource(0, constraintSource);
        }
    }
    private IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        body.velocity = dir * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && !collision.TryGetComponent<Proyectile>(out _))
        {
            if (collision.TryGetComponent(out Entity hitTarget))
            {
                hitTarget.GetComponent<HPManager>().Hurt(damage);
                DesactivateObject();
            }
            if (destroyOnContact)
            {
                DesactivateObject();
            }
            
        }
        
    }
    private void DesactivateObject()
    {
        if (keepEffects)
        {
            GetComponentInChildren<Collider2D>().enabled=false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void ActivateObject()
    {
        GetComponentInChildren<Collider2D>().enabled = true;
    }
    protected GameObject GetOwner()
    {
        return owner;
    }
}
