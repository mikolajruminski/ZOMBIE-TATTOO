using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFuryTime : ConsumableScript
{
    [SerializeField] private float furyTime = 5f;
    public override void OnDestroy()
    {
        Debug.Log("fury time");
        WeaponManagerScript.Instance.StartCoroutine(WeaponManagerScript.Instance.FuryTimeForAllWeapons(furyTime));
    }
}
