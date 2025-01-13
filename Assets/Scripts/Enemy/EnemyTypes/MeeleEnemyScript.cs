using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.IO;
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
        if (nav.hasPath)
        {
            var dir = (nav.steeringTarget - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);

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
        onLostLeg?.Invoke(this, EventArgs.Empty);
    }

    private void OnDrawGizmos()
    {
        for (var i = 0; i < nav.path.corners.Length - 1; i++)
        {
            Debug.DrawLine(nav.path.corners[i], nav.path.corners[i + 1], Color.yellow);
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }


}
