using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFuryTime : ConsumableScript
{
    [SerializeField] private float furyTime = 5f;
    public override void OnRemoved()
    {
        ConsumableHolderScript.Instance.ConsumableShot(gameObject);
        Debug.Log("consuambel shot spawning");

        Destroy(gameObject);
    }

    public override void OnUse()
    {
        PlayerUpgradeScript.Instance.ConsumedFuryTime(furyTime);
        Debug.Log("used fury pack");
        Destroy(gameObject);
    }
}
