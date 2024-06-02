using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public event EventHandler onMoneyChanged;
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int playerMoney;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int ReturnPlayerMoney()
    {
        return playerMoney;
    }

    public void AddMoney(int amountOfMoney)
    {
        playerMoney += amountOfMoney;
        onMoneyChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SubtractMoney(int amountOfMoney)
    {
        playerMoney -= amountOfMoney;
        onMoneyChanged?.Invoke(this, EventArgs.Empty);

    }
}
