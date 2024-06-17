using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConHealthPack : ConsumableScript
{
    [SerializeField] private int amountOfHeal = 10;
    public override void OnDestroyed()
    {
        FotelHealthScript.Instance.HealPlayer(amountOfHeal);
        
        Destroy(gameObject);
    }
}
