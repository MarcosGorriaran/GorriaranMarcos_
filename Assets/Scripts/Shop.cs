using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Shop : MonoBehaviour
{

    [SerializeField]
    ShopOffer[] offers;
    [SerializeField]
    WeaponSO[] weapons;
    Canvas canvas;

    private void Start()
    {
        Player.instance.scoreValueChanged += CheckToOpen;
        canvas = GetComponent<Canvas>();
    }
    private void CheckToOpen(int score)
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>().Where(obj=>obj.gameObject.activeSelf).ToArray();

        if(enemies.Length <= 1)
        {
            Open();
        }
    }
    private void Open()
    {
        Player.instance.scoreValueChanged -= CheckToOpen;
        ShuffleStore();
        canvas.enabled = true;
        Time.timeScale = 0;
        Player.instance.enabled = false;
        CrosshairTopDown.instance.enabled = false;
        PauseGame.instance.enabled = false;
    }
    private void ShuffleStore()
    {
        WeaponSO[] randomizedWeapons = weapons.OrderBy(_=>UnityEngine.Random.Range(0, weapons.Length)).ToArray();
        for (int i = 0; i < offers.Length; i++)
        {
            offers[i].WeaponToSell = randomizedWeapons[i];
            offers[i].UpdateOfferInfo();
        }
    }
    public void Close()
    {
        Player.instance.scoreValueChanged += CheckToOpen;
        canvas.enabled = false;
        Time.timeScale = 1;
        Player.instance.enabled = true;
        CrosshairTopDown.instance.enabled = true;
        PauseGame.instance.enabled = true;
    }

}
