using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int speed;
    [SerializeField] private int noLegSpeed;

    private EnemyConsumableDropScript enemyConsumableDropScript;

    public event EventHandler OnNoHeadDeath;
    public event EventHandler OnDeath;
    public event EventHandler noLegDeath;

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
            Death(DeathType.RegularDeath);
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

    public void Death(DeathType deathType)
    {
        if (enemyConsumableDropScript != null)
        {
            enemyConsumableDropScript.DropConsumable();
        }

        GameManager.Instance.AddEnemyKills();
        MoneyManager.Instance.AddMoney(GetComponent<BaseEnemyAI>().GetGoldValue());
        SpecialMeter.Instance.FillSpecialMeter(GetComponent<BaseEnemyAI>().GetPointValue());

        switch (deathType)
        {
            case DeathType.RegularDeath:
                if (GetComponentInChildren<LimbsMissingScript>().isLegMissing())
                {
                    noLegDeath?.Invoke(this, EventArgs.Empty);
                    break;
                }
                else
                {
                    OnDeath?.Invoke(this, EventArgs.Empty);
                    break;
                }

            case DeathType.NoHeadDeath:

                if (GetComponentInChildren<LimbsMissingScript>().isLegMissing())
                {
                    noLegDeath?.Invoke(this, EventArgs.Empty);
                    break;
                }
                else
                {
                    OnNoHeadDeath?.Invoke(this, EventArgs.Empty);
                    break;
                }

        }
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

    public enum DeathType
    {
        RegularDeath, NoHeadDeath, NoLegDeath
    }

}
