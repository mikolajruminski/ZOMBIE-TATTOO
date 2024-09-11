using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
public class MeeleEnemyScript : BaseEnemyAI
{
    private Rigidbody rb;
    private Vector3 og_destination;

    public event EventHandler onLostLeg;

    // Start is called before the first frame update
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        nav.speed = enemyScript.GetSpeed();
        fotelTransform = FotelHealthScript.Instance.transform.position;
        SetInitialCoordinates();
        og_destination = fotelTransform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            transform.LookAt(nav.destination);
        }
        else
        {
            transform.LookAt(og_destination);
        }

        /*
                if (GameManager.Instance.IsGameActive())
                {
                    if (isAttacking == false)
                    {
                        if (!destinationSet)
                        {
                            spread = UnityEngine.Random.Range(-4, 4);
                            destinationSet = true;
                        }

                        nav.destination = fotelTransform + new Vector3(0, 0, spread);

                    }
                }
                */


        if (isAttacking == false)
        {
            StoppingDistance();
        }

    }

    private void SetInitialCoordinates()
    {
        spread = UnityEngine.Random.Range(-4, 4);
        destinationSet = true;
        nav.destination = fotelTransform + new Vector3(0, 0, spread);
    }

    protected override void Attack()
    {
        CallAttackEvent();
    }

    public void MeeleAttack()
    {
        DealDamage();
    }

    /*   private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.TryGetComponent(out FotelScript fotelScript))
           {
               if (isAttacking == false)
               {
                   nav.isStopped = true;
                   isAttacking = true;
                   InvokeRepeating("Attack", 1, afterAttackCooldown);
               }
           }
       }
       */


    private void StoppingDistance()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            nav.isStopped = true;
            isAttacking = true;
            GetComponent<Rigidbody>().isKinematic = true;
            nav.SetDestination(transform.position);

            if (isAttacking)
            {
                Debug.Log("attacking");
                InvokeRepeating("Attack", 1, afterAttackCooldown);

            }
        }
    }

    public void SetLimbedDamage()
    {
        this.damage = 1;
        Debug.Log("set damage to 1, confirmation: damage = " + this.damage);
    }

    public void SetLostLegSpeed()
    {
        Debug.Log("lowered enemy speed, confirmation: previus speed " + nav.speed);
        nav.speed = 1;
        Debug.Log("current speed = " + nav.speed);
        onLostLeg?.Invoke(this, EventArgs.Empty);
    }


}
