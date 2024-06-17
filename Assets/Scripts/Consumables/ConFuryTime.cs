using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFuryTime : ConsumableScript
{
    [SerializeField] private float furyTime = 5f;
    public override void OnDestroyed()
    {
        PlayerUpgradeScript.Instance.StartCoroutine(PlayerUpgradeScript.Instance.ActivateFuryTime(furyTime));
        // Destroy(gameObject);
    }
}
