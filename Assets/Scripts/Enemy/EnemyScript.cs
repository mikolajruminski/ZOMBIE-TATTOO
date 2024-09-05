using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int speed;

    private EnemyConsumableDropScript enemyConsumableDropScript;

    public event EventHandler OnNoHeadDeath;
    public event EventHandler OnDeath;

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
        Destroy(gameObject);
    }

}
