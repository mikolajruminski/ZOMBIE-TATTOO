using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.Mathematics;

public class RangedEnemy : BaseEnemyAI
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }
    void Start()
    {
        nav.speed = enemyScript.GetSpeed();
        fotelTransform = FotelHealthScript.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
        TargetInRange();
    }

    protected override void Attack()
    {
        CallAttackEvent();
    }

    public void RangedAttack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void TargetInRange()
    {
        Vector3 direction = nav.destination - transform.position;

        Debug.DrawRay(transform.position, direction * 1000f, Color.yellow);
        if (Physics.SphereCast(transform.position, GetComponent<CapsuleCollider>().radius, direction, out RaycastHit hitinfo))
        {
            if (hitinfo.collider.TryGetComponent(out FotelHealthScript fotelHealthScript))
            {
                if (isAttacking == false)
                {
                    nav.isStopped = true;
                    isAttacking = true;
                    InvokeRepeating("Attack", 1, afterAttackCooldown);
                }
            }
        }
    }
}
