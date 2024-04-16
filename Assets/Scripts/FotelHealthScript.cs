using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotelHealthScript : MonoBehaviour
{
    public event EventHandler onHealthChanged;
    public static FotelHealthScript Instance { get; private set; }
    [SerializeField] private float health;

    private void Awake()
    {
        onHealthChanged?.Invoke(this, EventArgs.Empty);
        Instance = this;
    }


    public void TakeDamage(int damage)
    {
        Debug.Log(health);
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
}
