using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        OnDestroyed();
    }

    public virtual void OnDestroyed()
    {

    }

    public virtual void OnUse()
    {

    }

}
