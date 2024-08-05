using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkMachineAttackScript : MonoBehaviour
{
    private bool canDealDamage = false;
    [SerializeField] private int tickDamage = 1;
    [SerializeField] private float timeBetweenTicks = 0.2f;
    private List<IDamageable> damageables = new List<IDamageable>();
    // Start is called before the first frame update
    void Start()
    {
        GameArmsAnimatorScript.Instance.onInkAttackStart += onInkAttackStart;
    }

    private void onInkAttackStart(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDealingDamage()
    {
        InvokeRepeating("DealDamage", 0, timeBetweenTicks);
    }

    public void StopDealingDamage()
    {
        CancelInvoke();
        damageables.Clear();
    }

    private void DealDamage()
    {
        foreach (IDamageable damageable in damageables)
        {
            damageable.TakeDamage(tickDamage);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage)
        {
            if (other.TryGetComponent(out IDamageable idamageable))
            {
                damageables.Add(idamageable);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            damageables.Remove(idamageable);
        }
    }

}
