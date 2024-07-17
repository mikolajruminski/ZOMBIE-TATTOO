using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyScript : BaseEnemyAI
{

    [SerializeField] private GameObject WeakPoint;
    private Transform currentWeakPointPosition;
    [SerializeField] GameObject bossBody;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }

    void Start()
    {
        nav.speed = enemyScript.GetSpeed();
        fotelTransform = FotelHealthScript.Instance.transform.position;

        SetInitialCoordinates();
    }

    void Update()
    {
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
            if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
            {
                nav.isStopped = true;
                isAttacking = true;
                InvokeRepeating("Attack", 1, afterAttackCooldown);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public void NewWeakPoint()
    {
        Transform[] colliders = bossBody.GetComponentsInChildren<Transform>();
        int x = Random.Range(0, colliders.Length);
        int y = 0;

        Vector3 newWeakPointPosition;
        Transform newColliderTransform = colliders[x];

        if (newColliderTransform == currentWeakPointPosition)
        {

            while (newColliderTransform == currentWeakPointPosition && currentWeakPointPosition.position != null)
            {
                y = Random.Range(0, colliders.Length);
                newColliderTransform = colliders[y];
            }

            newWeakPointPosition = new Vector3(WeakPoint.transform.position.x, colliders[y].transform.position.y, colliders[y].transform.position.z);
        }
        else
        {


            newWeakPointPosition = new Vector3(WeakPoint.transform.position.x, colliders[x].transform.position.y, colliders[x].transform.position.z);
        }


        WeakPoint.transform.position = newWeakPointPosition;
        currentWeakPointPosition = newColliderTransform;
    }

}
