using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    private List<Proyectile> pool = new List<Proyectile>();
    public Proyectile ammo;
    public float spread;
    public float roundPerSeconds;
    public float muzzleOffset;
    public Transform bulletTrackTarget;
    public AudioSource fireSound;
    private Coroutine cooldown;
    protected Coroutine Cooldown 
    { 
        get
        {
            return cooldown;
        }
        private set 
        {
            cooldown = value;
        } 
    }
    public void GetWeapon(WeaponSO weapon)
    {
        spread = weapon.spread;
        ammo = weapon.ammo;
        roundPerSeconds = weapon.roundPerSeconds;
        fireSound.clip = weapon.weaponSound;
        pool.ForEach(obj => Destroy(obj.gameObject));
        pool = new List<Proyectile>();
        if (weapon.trackProyectile)
        {
            GetNewBulletTrackObject();
        }
    }
    private void OnDestroy()
    {
        foreach (Proyectile obj in pool)
        {
            Destroy(obj.gameObject);
        }
    }
    public void GetNewBulletTrackObject()
    {
        if(GetComponentsInChildren<RotateFollowParentAndTarget>().Count()>0)
            bulletTrackTarget = GetComponentInChildren<RotateFollowParentAndTarget>().transform;
    }
    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(roundPerSeconds);
        cooldown = null;
    }
    public virtual bool Fire(Vector2 source, Vector2 target, GameObject owner)
    {
        if(cooldown == null)
        {
            cooldown = StartCoroutine(StartCooldown());
            
            SpawnBullet(InstantiateProyectile(owner), source, target, owner);
            fireSound.Play();
            return true;
        }
        return false;
        
    }
    private Proyectile InstantiateProyectile(GameObject parent)
    {
        List<Proyectile> availableProyectiles = pool.Where(obj => !obj.gameObject.activeSelf).ToList();
        Proyectile firedBullet;
        if (availableProyectiles.Count > 0)
        {
            firedBullet = availableProyectiles.First();

        }
        else
        {
            firedBullet = Instantiate(ammo);
            firedBullet.gameObject.SetActive(false);
            DontDestroyOnLoad(firedBullet);
            pool.Add(firedBullet);

        }
        return firedBullet;
    }
    private float GetRandomSpread()
    {
        return Random.Range(spread *-1, spread);
    }
    private void SpawnBullet(Proyectile firedBullet, Vector2 source, Vector2 target, GameObject owner)
    {
        Vector2 spread = new Vector2(GetRandomSpread(), GetRandomSpread());
        Vector2 direction = ((target - source).normalized+spread).normalized;
        firedBullet.transform.position = source+(direction*muzzleOffset);

        float angle = Mathf.Atan2(direction.y, direction.x);
        firedBullet.transform.rotation = Quaternion.Euler(0,0,angle*180f/Mathf.PI-90f);

        if (bulletTrackTarget != null)
        {
            firedBullet.SetTrackTarget(bulletTrackTarget);
        }
        firedBullet.SetDirection(direction);
        firedBullet.SetOwner(owner);
        firedBullet.gameObject.SetActive(true);
    }
}
