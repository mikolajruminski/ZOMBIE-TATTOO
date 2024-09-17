using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConAmmoPack : ConsumableScript
{
    public override void OnRemoved()
    {
        ConsumableHolderScript.Instance.ConsumableShot(gameObject);
        Debug.Log("used ammo pack");
        Destroy(gameObject);
    }

    public override void OnUse()
    {
        GameManager.Instance.GetActiveGun().InstaReload();
        Debug.Log("used ammo pack");
        Destroy(gameObject);
    }
}
