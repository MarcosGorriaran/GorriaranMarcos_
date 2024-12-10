using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Proyectile : MonoBehaviour
{
    Vector2 dir = new Vector2(0,0);
    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime;
    [SerializeField]
    uint damage;
    [SerializeField]
    bool explodeOnEnd;
    [SerializeField]
    float explosionRadius;
    [SerializeField]
    float explosionForce;
    [SerializeField]
    uint explosionDamage;
    Rigidbody2D body;
    private void OnDisable()
    {
        if (explodeOnEnd)
        {
            Physics2D.OverlapCircleAll(transform.position,explosionRadius);
        }
    }
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 direction)
    {
        dir = direction;
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
        if (collision.TryGetComponent(out Entity hitTarget))
        {
            hitTarget.GetComponent<HPManager>().Hurt(damage);
        }
        gameObject.SetActive(false);
    }
}
