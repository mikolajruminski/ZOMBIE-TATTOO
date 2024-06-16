using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        OnDestroy();
    }

    public virtual void OnDestroy()
    {

    }

}
