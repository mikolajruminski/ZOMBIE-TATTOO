using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler gameIsActive;
    public event EventHandler onKill;
    public event EventHandler onRoundChage;
    [SerializeField] private GameObject gamePlayer, preGamePlayer;
    private bool isGameActive = false;
    [SerializeField] private GameObject[] meeleSpawners, rangedSpawners;
    [SerializeField] private GameObject meeleEnemyPrefab, rangedEnemyPrefab;
    [SerializeField] private float baseEnemyRespawnRate;

    [SerializeField] int baseMaxEnemies = 10;
    [SerializeField] private int baseQuantityOfKillsToWin = 15;

    private int maxEnemies;
    private int quantityOfKillsToWin;

    private float enemyRespawnRate;

    private int roundCount = 0;
    private int enemiesLeft;

    private GunSystem activeGun;

    [SerializeField] private int timeBetweenRounds = 5;

    private void Awake()
    {
        gamePlayer.SetActive(false);
        Instance = this;
    }
    public void SwitchGameMode()
    {
        if (WeaponManagerScript.Instance.IsStartingWeaponChosen())
        {
            StartGame();
        }
        else
        {
            Debug.Log("pick starting weapon!");
        }

    }

    private void StartGame()
    {
        preGamePlayer.SetActive(false);
        gamePlayer.SetActive(true);
        isGameActive = true;

        maxEnemies = baseMaxEnemies;
        quantityOfKillsToWin = baseQuantityOfKillsToWin;
        enemyRespawnRate = baseEnemyRespawnRate;


        InvokeSpawningEnemies();

        gameIsActive?.Invoke(this, EventArgs.Empty);
        IncreaseRound();
    }

    public void GameOver()
    {
        // Destroy(PlayerController.Instance);
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    private void InvokeSpawningEnemies()
    {
        if (isGameActive)
        {
            InvokeRepeating("SpawnEnemies", 2, enemyRespawnRate);

        }
    }

    private void SpawnEnemies()
    {
        BaseEnemyAI[] enemies = GameObject.FindObjectsOfType<BaseEnemyAI>();
        enemiesLeft = enemies.Length;

        if (enemies.Length < maxEnemies && enemies.Length < quantityOfKillsToWin)
        {
            int x = UnityEngine.Random.Range(0, rangedSpawners.Length);
            Instantiate(rangedEnemyPrefab, rangedSpawners[x].transform.position, Quaternion.identity);
        }
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

        if (quantityOfKillsToWin < 1)
        {
            YouWin();
        }
    }

    private void YouWin()
    {
        CancelInvoke("SpawnEnemies");

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
        onRoundChage?.Invoke(this, EventArgs.Empty);
        Debug.Log("Round number:" + roundCount);
    }

    private IEnumerator waitBetweenRounds()
    {
        Debug.Log("break time");
        float x = 0;
        while (x < timeBetweenRounds)
        {
            x += Time.deltaTime;
            Debug.Log(x);

            yield return null;
        }

        StartNewRound();
    }

    private void StartNewRound()
    {
        IncreaseRound();
        quantityOfKillsToWin = roundCount * baseQuantityOfKillsToWin;
        maxEnemies = roundCount * baseMaxEnemies;
        enemyRespawnRate = baseEnemyRespawnRate / roundCount;


        InvokeSpawningEnemies();
    }

    public int ReturnCurrentRound()
    {
        return roundCount;
    }



}
