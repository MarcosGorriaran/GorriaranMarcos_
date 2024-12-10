using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSO : MonoBehaviour
{
    private List<Proyectile> pool = new List<Proyectile>();
    public Proyectile ammo;
    public float spread;
    public float roundPerSeconds;
    private Coroutine cooldown;

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(roundPerSeconds);
        cooldown = null;
    }
    public bool Fire(Vector2 source, Vector2 target)
    {
        if(cooldown != null)
        {
            cooldown = StartCoroutine(Cooldown());
            List<Proyectile> availableProyectiles = pool.Where(obj => !obj.gameObject.activeSelf).ToList();
            if (availableProyectiles.Count > 0)
            {
                Proyectile firedBullet = availableProyectiles.First();
                SpawnBullet(firedBullet, source, target);
            }
            else
            {
                Proyectile firedBullet = Instantiate(ammo);
                pool.Add(firedBullet);
                SpawnBullet(firedBullet, source, target);
            }
            return true;
        }
        return false;
        
    }
    private float GetRandomSpread()
    {
        return Random.Range(spread *-1, spread);
    }
    private void SpawnBullet(Proyectile firedBullet, Vector2 source, Vector2 target)
    {
        firedBullet.gameObject.SetActive(true);
        firedBullet.transform.position = source;
        firedBullet.SetDirection(new Vector2(target.x + GetRandomSpread(), target.y + GetRandomSpread()).normalized);
    }
}
