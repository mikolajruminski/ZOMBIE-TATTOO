using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkMachineAttackScript : MonoBehaviour
{
    public static InkMachineAttackScript Instance { get; private set; }
    [SerializeField] private int tickDamage = 1;
    [SerializeField] private float timeBetweenTicks = 0.3f;
    [SerializeField] private int attackDuration = 5;
    private List<IDamageable> damageables = new List<IDamageable>();

    public event EventHandler onInkAttackEnded;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDealingDamage()
    {
        damageables.Clear();
        InvokeRepeating("DealDamage", 0.02f, timeBetweenTicks);
        StartCoroutine(TimeStopDealingDamage());
    }

    public void StopDealingDamage()
    {
        onInkAttackEnded?.Invoke(this, EventArgs.Empty);

        CancelInvoke();

        damageables.Clear();
    }

    private void DealDamage()
    {
        foreach (IDamageable damageable in damageables)
        {
            damageable.TakeDamage(tickDamage);
        }

        damageables.Clear();
    }

    private IEnumerator TimeStopDealingDamage()
    {
        yield return new WaitForSeconds(attackDuration);
        StopDealingDamage();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            if (damageables.Count > 0)
            {
                if (damageables.Contains(idamageable))
                {

                }
                else
                {
                    damageables.Add(idamageable);
                }
            }
            else
            {
                damageables.Add(idamageable);
            }


        }

    }

    /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable idamageable))
            {
               damageables.Add(idamageable);
            }
        }
*/

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            damageables.Remove(idamageable);
        }
    }

}
