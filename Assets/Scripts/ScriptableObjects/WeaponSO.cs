using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 2)]
public class WeaponSO : ScriptableObject
{
    public Sprite weaponModel;
    public Proyectile ammo;
    public string description;
    public float spread;
    public float roundPerSeconds;
    public uint cost;
    public bool trackProyectile;
}
