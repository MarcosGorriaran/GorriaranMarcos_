using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ScriptableObject
{
    public SpriteRenderer weaponModel;
    public Proyectile ammo;
    public float spread;
    public float roundPerSeconds;
}
