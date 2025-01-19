using System.Linq;
using UnityEngine;

public class ExplosiveProyectile : Proyectile
{
    [SerializeField]
    bool explosionHarmsOwner;
    [SerializeField]
    float explosionRadius;
    [SerializeField]
    float explosionForce;
    [SerializeField]
    uint explosionDamage;
    protected override void OnDisable()
    {
        base.OnDisable();
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D collision in collisions.Where(obj=>obj.TryGetComponent<Entity>(out _)))
        {
            if(explosionHarmsOwner || collision.gameObject != GetOwner())
            {
                HPManager entityhp = collision.GetComponent<HPManager>();
                entityhp.Hurt(explosionDamage);
                Rigidbody2D entityPhysics = GetComponent<Rigidbody2D>();
                entityPhysics.AddForce((transform.position - collision.transform.position).normalized * explosionForce);
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
