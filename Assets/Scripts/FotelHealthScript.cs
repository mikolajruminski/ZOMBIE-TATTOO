using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotelHealthScript : MonoBehaviour
{
    public event EventHandler onHealthChanged;
    public static FotelHealthScript Instance { get; private set; }
    [SerializeField] private float health;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = health;
        onHealthChanged?.Invoke(this, EventArgs.Empty);
        Instance = this;
    }

    private void Start()
    {
        Mathf.Clamp(health, -1, maxHealth);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        onHealthChanged?.Invoke(this, EventArgs.Empty);

        if (health < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    public float ReturnFotelHealth()
    {
        return health;
    }

    public void HealPlayer(int amount)
    {
        if (health + amount < maxHealth)
        {
            health += amount;
        }
        else
        {
            health = maxHealth;
        }

        onHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseFotelHealth(float amount)
    {
        float value = maxHealth * (amount / 100);
        maxHealth += value;
    }
}
