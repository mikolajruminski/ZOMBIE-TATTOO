using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        OnRemoved();
    }

    public virtual void OnRemoved()
    {

    }

    public virtual void OnUse()
    {

    }

}
