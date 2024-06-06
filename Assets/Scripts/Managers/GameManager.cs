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
    public event EventHandler onShopOpened;
    [SerializeField] private GameObject gamePlayer, preGamePlayer;
    private bool isGameActive = false;
    [SerializeField] private GameObject[] meeleSpawners, rangedSpawners;
    [SerializeField] private GameObject meeleEnemyPrefab, rangedEnemyPrefab;
    [SerializeField] private float baseEnemyRespawnRate;

    [SerializeField] int baseMaxEnemies = 10;
    [SerializeField] private int baseQuantityOfKillsToWin = 15;

    private int maxEnemies;
    private int maxMeleeEnemies;
    private int quantityOfKillsToWin;

    private float enemyRespawnRate;

    private int roundCount = 0;
    private int enemiesLeft;

    private GunSystem activeGun;

    [SerializeField] private int timeBetweenRounds = 10;
    private bool isBreak = false;
    private float timePassedInBreak;

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
        MeeleEnemyScript[] meeleEnemies = GameObject.FindObjectsOfType<MeeleEnemyScript>();
        enemiesLeft = meeleEnemies.Length;

        maxMeleeEnemies = maxEnemies - rangedSpawners.Length;

        if (meeleEnemies.Length < maxMeleeEnemies && meeleEnemies.Length < quantityOfKillsToWin && meeleEnemyPrefab != null)
        {
            int x = UnityEngine.Random.Range(0, meeleSpawners.Length);
            Instantiate(meeleEnemyPrefab, meeleSpawners[x].transform.position, Quaternion.identity);
        }

        RangedEnemy[] rangedEnemies = GameObject.FindObjectsOfType<RangedEnemy>();

        if (rangedEnemies.Length < rangedSpawners.Length && rangedEnemies.Length < quantityOfKillsToWin && rangedEnemyPrefab != null)
        {
            int x = UnityEngine.Random.Range(0, rangedSpawners.Length);

            if (rangedSpawners[x].gameObject.GetComponent<SpawnerScript>().spawnedEnemy != null)
            {
                return;
            }
            else
            {
                GameObject spawnedEnemy = Instantiate(rangedEnemyPrefab, rangedSpawners[x].transform.position, Quaternion.identity);
                rangedSpawners[x].gameObject.GetComponent<SpawnerScript>().spawnedEnemy = spawnedEnemy.GetComponent<RangedEnemy>();
            }

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

    public void OpenShop()
    {
        if (isBreak)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                onShopOpened?.Invoke(this, EventArgs.Empty);
                PlayerController.Instance.SwitchCameraCanMove();
            }

            else
            {
                Debug.Log("cannot open shop during game!");
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

}
