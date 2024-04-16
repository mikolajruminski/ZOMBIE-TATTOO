using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyAI : MonoBehaviour
{
    public event EventHandler OnAttackPerformed;
    private EnemyScript enemyScript;
    private Transform fotelTransform;
    private NavMeshAgent nav;
    private bool isAttacking = false;
    [SerializeField] private float afterAttackCooldown = 4f;
    [SerializeField] private int damage = 1;
    private bool destinationSet = false;
    private float spread;
    [SerializeField] private float stopLength = 3f;
    // Start is called before the first frame update

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }
    void Start()
    {
        nav.speed = enemyScript.GetSpeed();
        fotelTransform = PlayerController.Instance.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameActive())
        {
            if (isAttacking == false && fotelTransform.position.x - transform.position.x > stopLength)
            {
                if (!destinationSet)
                {
                    spread = UnityEngine.Random.Range(-6, 6);
                    destinationSet = true;
                }

                nav.destination = fotelTransform.position + new Vector3(0, 0, spread);

            }
            else if (isAttacking == false && fotelTransform.position.x - transform.position.x < stopLength)
            {
                nav.isStopped = true;
                isAttacking = true;
                InvokeRepeating("Attack", 1, afterAttackCooldown);
            }
        }
    }

    private void Attack()
    {
        Debug.Log("attack");
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    public void DealDamage()
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


}
