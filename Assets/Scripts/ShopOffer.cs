using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopOffer : MonoBehaviour
{
    [SerializeField]
    Image spriteRenderer;
    [SerializeField]
    Text Description;
    [SerializeField]
    Text costShow;
    private WeaponSO weaponToSell;
    public WeaponSO WeaponToSell { 
        private get
        {
            return weaponToSell; 
        } 
        set
        {
            weaponToSell = value;
        }
    }

    public void ProvideWeapon()
    {
        if (Player.instance.PayWithPoints(weaponToSell.cost))
        {
            Player.instance.weapon.GetWeapon(weaponToSell);
        }
    }
    public void UpdateOfferInfo()
    {
        costShow.text = weaponToSell.cost.ToString();
        Description.text = weaponToSell.description.ToString();
        spriteRenderer.sprite = weaponToSell.weaponModel;
    }
}
