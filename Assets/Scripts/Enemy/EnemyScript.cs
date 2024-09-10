using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int speed;

    private EnemyConsumableDropScript enemyConsumableDropScript;

    public event EventHandler OnNoHeadDeath;
    public event EventHandler OnDeath;

    [SerializeField] private GameObject torsoObject;

    private void Start()
    {
        enemyConsumableDropScript = GetComponent<EnemyConsumableDropScript>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //add it to gamemanager, by an event maybe?
            GameManager.Instance.AddEnemyKills();
            MoneyManager.Instance.AddMoney(GetComponent<BaseEnemyAI>().GetGoldValue());
            SpecialMeter.Instance.FillSpecialMeter(GetComponent<BaseEnemyAI>().GetPointValue());
            RegularDeath();

            if (enemyConsumableDropScript != null)
            {
                enemyConsumableDropScript.DropConsumable();
            }

        }
    }

    public int GetSpeed()
    {
        return speed;
    }
    public void SetArmorSpeed(int speed)
    {
        this.speed = speed;
    }

    public void RegularDeath()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public void NoHeadDeath()
    {
        OnNoHeadDeath?.Invoke(this, EventArgs.Empty);
    }

    public void DestroyOnDeath()
    {
        this.enabled = false;
        if (GetComponent<MeeleEnemyScript>() != null)
        {
            GetComponent<MeeleEnemyScript>().enabled = false;
        }

        if (GetComponent<RangedEnemy>() != null)
        {
            GetComponent<RangedEnemy>().enabled = false;
        }

        GetComponent<EnemyStatusAligements>().enabled = false;
        GetComponent<EnemyAlimentsVisualScript>().enabled = false;
        GetComponent<EnemyConsumableDropScript>().enabled = false;
        GetComponent<EnemyImmunitiesScript>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<LimbFragmentationScript>().enabled = false;
        GetComponentInChildren<EnemyAnimator>().enabled = false;

        foreach (Transform child in torsoObject.transform)
        {
            if (child.GetComponent<LimbFragmentationScript>() != null)
            {
                child.GetComponent<LimbFragmentationScript>().SwitchOnDeath();
            }

            /*
            child.GetComponent<Collider>().enabled = false;
            Destroy(child.GetComponent<Rigidbody>());
            */
        }

    }

}
