using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler gameIsActive;
    public event EventHandler onKill;
    public event EventHandler onRoundChage;
    public event EventHandler onShopOpened;
    [SerializeField] private GameObject gamePlayer, preGamePlayer;
    private bool isGameActive = false;
    [SerializeField] private float baseEnemyRespawnRate;
    [SerializeField] int baseMaxEnemies = 10;
    [SerializeField] private int baseQuantityOfKillsToWin = 15;

    private int maxEnemies;
    private int quantityOfKillsToWin;

    private float enemyRespawnRate;

    private int roundCount = 0;
    private GunSystem activeGun;

    #region Shop

    [SerializeField] private int timeBetweenRounds = 10;

    private bool isBreak = false;
    private float timePassedInBreak;
    private bool isShopOpened;

    #endregion

    #region DebugCheckboxes
    [SerializeField] private bool canPlayerDie = false;

    #endregion

    private void Awake()
    {
        gamePlayer.SetActive(false);
        Instance = this;
    }
    public void SwitchGameMode()
    {
        StartGame();
    }

    private void StartGame()
    {
        preGamePlayer.SetActive(false);
        gamePlayer.SetActive(true);

        WeaponManagerScript.Instance.SetStartingWeapon();

        isGameActive = true;

        maxEnemies = baseMaxEnemies;
        quantityOfKillsToWin = baseQuantityOfKillsToWin;
        enemyRespawnRate = baseEnemyRespawnRate;


        InvokeSpawningEnemies();

        gameIsActive?.Invoke(this, EventArgs.Empty);
        IncreaseRound();
    }

    private void Update()
    {
        OpenShop();
    }

    public void GameOver()
    {
        if (canPlayerDie)
        {
            Destroy(PlayerController.Instance);
        }

    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    private void InvokeSpawningEnemies()
    {
        SpawnManager.Instance.InvokeSpawningEnemies(enemyRespawnRate);
    }


    public void SetActiveGun(GunSystem gun)
    {
        activeGun = gun;
    }

    public GunSystem GetActiveGun()
    {
        return activeGun;
    }


    public void AddEnemyKills()
    {
        quantityOfKillsToWin--;
        onKill?.Invoke(this, EventArgs.Empty);

        Mathf.Clamp(quantityOfKillsToWin, 0, quantityOfKillsToWin);
        if (quantityOfKillsToWin < 1)
        {
            YouWin();
        }
    }

    private void YouWin()
    {
        SpawnManager.Instance.CancelInvokeSpawningEnemies();

        BaseEnemyAI[] enemies = GameObject.FindObjectsOfType<BaseEnemyAI>();

        foreach (BaseEnemyAI enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        if (roundCount < 3)
        {
            StartCoroutine(waitBetweenRounds());
        }

        else
        {
            Debug.Log("game over");
        }

    }

    public int ReturnNumberOfKillsLeft()
    {
        return quantityOfKillsToWin;
    }

    private void IncreaseRound()
    {
        roundCount++;
        Debug.Log("Round number:" + roundCount);
    }

    private IEnumerator waitBetweenRounds()
    {
        isBreak = true;

        timePassedInBreak = 0;
        while (timePassedInBreak < timeBetweenRounds)
        {
            timePassedInBreak += Time.deltaTime;

            yield return null;
        }

        StartNewRound();
    }

    private void StartNewRound()
    {
        isBreak = false;

        PlayerController.Instance.SwitchCameraCanMove(true);

        IncreaseRound();

        quantityOfKillsToWin += roundCount * baseQuantityOfKillsToWin;
        maxEnemies = roundCount * baseMaxEnemies;
        enemyRespawnRate = baseEnemyRespawnRate / roundCount;

        onRoundChage?.Invoke(this, EventArgs.Empty);
        InvokeSpawningEnemies();
    }

    public int ReturnCurrentRound()
    {
        return roundCount;
    }

    public void OpenShop()
    {
        if (isBreak)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isShopOpened == false)
                {
                    isShopOpened = true;
                    onShopOpened?.Invoke(this, EventArgs.Empty);
                    PlayerController.Instance.SwitchCameraCanMove(false);
                }
                else
                {
                    isShopOpened = false;
                    onShopOpened?.Invoke(this, EventArgs.Empty);
                    PlayerController.Instance.SwitchCameraCanMove(true);
                }

            }
        }
    }

    public float ReturnBreakTimer()
    {
        return timePassedInBreak;
    }

    public bool ReturnIsBreak()
    {
        return isBreak;
    }

    public bool ReturnIsGameActive()
    {
        return isGameActive;
    }

    public int ReturnQuantityOfKillsToWin()
    {
        return quantityOfKillsToWin;
    }

    public int ReturnMaxEnemies()
    {
        return maxEnemies;
    }

}
