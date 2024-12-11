using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Proyectile : MonoBehaviour
{
    Vector2 dir = new Vector2(0,0);
    GameObject owner;
    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime;
    [SerializeField]
    uint damage;
    Rigidbody2D body;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
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
        if (collision.gameObject != owner)
        {
            if (collision.TryGetComponent(out Entity hitTarget))
            {
                hitTarget.GetComponent<HPManager>().Hurt(damage);
            }
            gameObject.SetActive(false);
        }
        
    }
    protected GameObject GetOwner()
    {
        return owner;
    }
}
