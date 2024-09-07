using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.Mathematics;

public class RangedEnemy : BaseEnemyAI
{
    // Start is called before the first frame update
    [SerializeField] private GameObject regularBulletPrefab;
    [SerializeField] private GameObject[] specialBulletsPrefabs;

    private GameObject bulletPrefab;

    private Vector3 yOffset = new Vector3(0, 1.5f, 0);

    #region Random bullets values

    [SerializeField] private float regularBulletChance = 0.7f;

    private Transform og_destination;

    #endregion

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }
    void Start()
    {
        RandomizeBulletPrefab();
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
        Instantiate(bulletPrefab, transform.position + yOffset, Quaternion.identity);
    }

    private void TargetInRange()
    {
        Vector3 direction = nav.destination - transform.position;

        Debug.DrawRay(transform.position, direction * 1000f, Color.yellow);
        if (Physics.SphereCast(transform.position, GetComponentInChildren<CapsuleCollider>().radius, direction, out RaycastHit hitinfo))
        {
            if (hitinfo.collider.TryGetComponent(out FotelHealthScript fotelHealthScript))
            {
                if (isAttacking == false)
                {
                    nav.SetDestination(transform.position);
                    nav.isStopped = true;
                    isAttacking = true;
                    InvokeRepeating("Attack", 1, afterAttackCooldown);
                }
            }
        }
    }

    private void RandomizeBulletPrefab()
    {
        float randVal = UnityEngine.Random.value;
        if (randVal < regularBulletChance)
        {
            bulletPrefab = regularBulletPrefab;
        }
        else
        {
            int x = UnityEngine.Random.Range(0, specialBulletsPrefabs.Length);

            bulletPrefab = specialBulletsPrefabs[x];
        }

    }


}
