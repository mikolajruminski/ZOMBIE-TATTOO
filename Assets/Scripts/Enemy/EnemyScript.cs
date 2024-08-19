using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int speed;

    private EnemyConsumableDropScript enemyConsumableDropScript;

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


            if (enemyConsumableDropScript != null)
            {
                enemyConsumableDropScript.DropConsumable();
            }


            Destroy(gameObject);
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
}
