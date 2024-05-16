using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    public event EventHandler OnAttackPerformed;

    protected Vector3 fotelTransform;
    protected EnemyScript enemyScript;
    protected NavMeshAgent nav;
    protected bool isAttacking = false;
    [SerializeField] protected float afterAttackCooldown = 4f;
    [SerializeField] protected int damage = 1;
    protected bool destinationSet = false;
    protected float spread;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    protected virtual void Attack()
    {

    }

    protected void DealDamage()
    {
        FotelHealthScript.Instance.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        if (nav != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(nav.destination, 0.5f);
        }
    }

    protected void CallAttackEvent()
    {
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }
}
