using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI currentHP;
    [SerializeField] private TextMeshProUGUI enemyKillCount;
    [SerializeField] private TextMeshProUGUI timeBetweenRoundsText;
    [SerializeField] private TextMeshProUGUI currentRound;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject furyTimeOverlay;

    [SerializeField] private GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        MoneyManager.Instance.onMoneyChanged += MoneyManager_Instance_onMoneyChanged;
        GameManager.Instance.gameIsActive += GameManager_Instance_GameIsActive;
        GameManager.Instance.onKill += GameManager_Instance_OnKill;
        GameManager.Instance.onRoundChage += GameManager_Instance_OnRoundChage;
        FotelHealthScript.Instance.onHealthChanged += FotelHealthScript_Instance_OnHealthChanged;
        GameManager.Instance.onShopOpened += GameManager_Instance_OnShopOpened;
        WeaponManagerScript.Instance.onFuryTimeEnabled += WeaponManager_Instance_OnFuryTime;
        WeaponManagerScript.Instance.onFuryTimeDisabled += WeaponManager_Instance_OnFuryTimeDisabled;

        gameObject.SetActive(false);
    }

    private void WeaponManager_Instance_OnFuryTimeDisabled(object sender, EventArgs e)
    {
        furyTimeOverlay.gameObject.SetActive(false);
    }

    private void WeaponManager_Instance_OnFuryTime(object sender, EventArgs e)
    {
        furyTimeOverlay.gameObject.SetActive(true);
    }

    private void GameManager_Instance_OnShopOpened(object sender, EventArgs e)
    {
        shopUI.gameObject.SetActive(!shopUI.gameObject.activeInHierarchy);
    }

    private void MoneyManager_Instance_onMoneyChanged(object sender, EventArgs e)
    {
        playerGold.text = "Gold: " + MoneyManager.Instance.ReturnPlayerMoney();
    }

    private void GameManager_Instance_OnRoundChage(object sender, EventArgs e)
    {
        currentRound.text = "" + GameManager.Instance.ReturnCurrentRound();
        enemyKillCount.text = "" + GameManager.Instance.ReturnNumberOfKillsLeft();
    }

    private void GameManager_Instance_OnKill(object sender, EventArgs e)
    {
        enemyKillCount.text = "" + GameManager.Instance.ReturnNumberOfKillsLeft();
    }

    private void GameManager_Instance_GameIsActive(object sender, EventArgs e)
    {
        playerUI.gameObject.SetActive(true);
        currentHP.text = "" + FotelHealthScript.Instance.ReturnFotelHealth();
        enemyKillCount.text = "" + GameManager.Instance.ReturnNumberOfKillsLeft();
    }

    private void FotelHealthScript_Instance_OnHealthChanged(object sender, EventArgs e)
    {
        currentHP.text = "" + FotelHealthScript.Instance.ReturnFotelHealth();
    }


    private void Update()
    {
        UpdateAmmoStats();
        UpdateBreakTimer();
    }

    private void UpdateAmmoStats()
    {
        if (GameManager.Instance.IsGameActive())
        {
            ammoText.text = GameManager.Instance.GetActiveGun().ReturnMagazineSize() + "/" +
            GameManager.Instance.GetActiveGun().ReturnCurrentAmmo();
        }
    }

    private void UpdateBreakTimer()
    {
        if (GameManager.Instance.ReturnIsBreak())
        {
            timeBetweenRoundsText.gameObject.SetActive(true);
            timeBetweenRoundsText.text = "Break Time: " + (int)GameManager.Instance.ReturnBreakTimer();
        }
        else
        {
            timeBetweenRoundsText.gameObject.SetActive(false);
            shopUI.gameObject.SetActive(false);
        }
    }

}
