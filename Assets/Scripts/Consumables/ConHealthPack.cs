using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConHealthPack : ConsumableScript
{
    [SerializeField] private int amountOfHeal = 10;
    public override void OnDestroyed()
    {
        ConsumableHolderScript.Instance.ConsumableShot(gameObject);
        Debug.Log("used health pack");
        Destroy(gameObject);
    }
    public override void OnUse()
    {
        FotelHealthScript.Instance.HealPlayer(amountOfHeal);
        Debug.Log("used ammo pack");
        Destroy(gameObject);
    }
}
