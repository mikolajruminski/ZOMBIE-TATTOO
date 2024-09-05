using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
public class MeeleEnemyScript : BaseEnemyAI
{
    private Rigidbody rb;

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


    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(nav.destination);
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


}
