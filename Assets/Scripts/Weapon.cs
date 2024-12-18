using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private List<Proyectile> pool = new List<Proyectile>();
    public Proyectile ammo;
    public float spread;
    public float roundPerSeconds;
    public float muzzleOffset;
    private Coroutine cooldown;

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(roundPerSeconds);
        cooldown = null;
    }
    public bool Fire(Vector2 source, Vector2 target, GameObject owner)
    {
        if(cooldown == null)
        {
            cooldown = StartCoroutine(Cooldown());
            List<Proyectile> availableProyectiles = pool.Where(obj => !obj.gameObject.activeSelf).ToList();
            if (availableProyectiles.Count > 0)
            {
                Proyectile firedBullet = availableProyectiles.First();
                SpawnBullet(firedBullet, source, target, owner);
            }
            else
            {
                Proyectile firedBullet = Instantiate(ammo);
                pool.Add(firedBullet);
                SpawnBullet(firedBullet, source, target, owner);
            }
            return true;
        }
        return false;
        
    }
    private float GetRandomSpread()
    {
        return Random.Range(spread *-1, spread);
    }
    private void SpawnBullet(Proyectile firedBullet, Vector2 source, Vector2 target, GameObject owner)
    {
        firedBullet.gameObject.SetActive(true);
        Vector2 direction = (target - source).normalized;
        firedBullet.transform.position = source+(direction*muzzleOffset);
        float angle = Mathf.Atan2(direction.y, direction.x);
        firedBullet.transform.rotation = Quaternion.Euler(0,0,angle*180f/Mathf.PI-90f);
        firedBullet.SetDirection(direction);
        firedBullet.SetOwner(owner);
    }
}
