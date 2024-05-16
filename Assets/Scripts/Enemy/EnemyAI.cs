using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyAI : MonoBehaviour
{
    public event EventHandler OnAttackPerformed;
    protected EnemyScript enemyScript;
    protected Vector3 fotelTransform;
    protected NavMeshAgent nav;
    protected bool isAttacking = false;
    [SerializeField] protected float afterAttackCooldown = 4f;
    [SerializeField] protected int damage = 1;
    protected bool destinationSet = false;
    protected float spread;
    // Start is called before the first frame update

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

    private void OnTriggerEnter(Collider other)
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


}
