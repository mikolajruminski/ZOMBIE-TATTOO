using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConAmmoPack : ConsumableScript
{
    public override void OnDestroyed()
    {
        GameManager.Instance.GetActiveGun().InstaReload();

        Destroy(gameObject);
    }
}
