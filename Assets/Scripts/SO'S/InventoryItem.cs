using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject, IConsumable
{
    public ShopManager.ShopItems ShopItem;
    public ItemType itemType;
    public ConsumableType consumableType;
    public Sprite sprite;
    public string itemName;
    public int itemPrice;

    public int itemAmount;
    public string description;


    public void UseConsumable()
    {
        switch (consumableType)
        {
            case ConsumableType.Ammo:
                Debug.Log("given ammo");
                break;
            case ConsumableType.Health:
                Debug.Log("given health");
                FotelHealthScript.Instance.HealPlayer(itemAmount);
                break;
        }
    }

    public enum ItemType
    {
        Consumable, Upgrade
    }

    public enum ConsumableType
    {
        Ammo, Health
    }

    public enum WeaponUpgrade
    {
        AmmoExtend, RateOfFireExtend, DamageIncrease, ReloadSpeedDecrease
    }
}
