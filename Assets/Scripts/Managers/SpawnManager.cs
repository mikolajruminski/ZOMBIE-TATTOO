using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private GameObject[] meeleSpawners, rangedSpawners;
    [SerializeField] private GameObject meeleEnemyPrefab, rangedEnemyPrefab, bossPrefab;

    MeeleEnemyScript[] meeleEnemiesDebug;
    RangedEnemy[] rangedEnemiesDebug;

    private int quanityOfKillsThisRound;

    #region Boss
    private float currentBossChance;
    [SerializeField] private float[] chanceToSpawnBoss = { 0.03f, 0.05f, 0.08f, 0.1f };
    private float[] bossSpawnChanceRoundMultiplier = { 1, 1.2f, 2f };
    private float currentRoundMultiplier;

    #endregion
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentBossChance = chanceToSpawnBoss[0];
        GameManager.Instance.onRoundChage += GameManager_Instance_onRoundChage;
        GameManager.Instance.onKill += GameManager_Instance_onKill;
    }

    private void GameManager_Instance_onKill(object sender, EventArgs e)
    {
        ChangeBossChance();
    }

    private void GameManager_Instance_onRoundChage(object sender, EventArgs e)
    {
        currentRoundMultiplier = GameManager.Instance.ReturnCurrentRound() - 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyCount();
    }

    public void SpawnEnemies()
    {
        MeeleEnemyScript[] meeleEnemies = GameObject.FindObjectsOfType<MeeleEnemyScript>();
        RangedEnemy[] rangedEnemies = GameObject.FindObjectsOfType<RangedEnemy>();
        BossEnemyScript[] bossEnemies = FindObjectsOfType<BossEnemyScript>();

        int currentAmountOfEnemies = meeleEnemies.Length + rangedEnemies.Length;
        int amountOfEnemiesToSpawn = GameManager.Instance.ReturnMaxEnemies() - currentAmountOfEnemies;
        int amountOfRangedToSpawn = rangedSpawners.Length - rangedEnemies.Length;
        int amountOfMeeleEnemiesToSpawn = amountOfEnemiesToSpawn - amountOfRangedToSpawn;

        int currentlySpawnedMeeleEnemies = 0;
        int currentlySpawnedRangedEnemies = 0;

        Debug.Log("need to spawn " + amountOfMeeleEnemiesToSpawn + " meele enemies + " + amountOfRangedToSpawn + " ranged enemies");

        if (currentAmountOfEnemies < GameManager.Instance.ReturnMaxEnemies() && currentAmountOfEnemies < GameManager.Instance.ReturnQuantityOfKillsToWin())
        {
            if (amountOfRangedToSpawn > 0 && currentAmountOfEnemies + amountOfRangedToSpawn <= GameManager.Instance.ReturnQuantityOfKillsToWin())
            {
                int x = UnityEngine.Random.Range(0, 10);

                if (x < 2)
                {
                    Debug.Log("chance to spawn ranged enemy: " + x + "/10, spawning all possible ranged enemies");
                    for (int i = 0; i <= amountOfRangedToSpawn; i++)
                    {
                        int y = UnityEngine.Random.Range(0, rangedSpawners.Length);

                        if (rangedSpawners[y].gameObject.GetComponent<SpawnerScript>().spawnedEnemy != null)
                        {
                            for (int j = 0; j < rangedSpawners.Length; j++)
                            {
                                if (rangedSpawners[j].gameObject.GetComponent<SpawnerScript>().spawnedEnemy == null)
                                {
                                    currentlySpawnedRangedEnemies++;
                                    GameObject spawnedEnemy = Instantiate(rangedEnemyPrefab, rangedSpawners[j].transform.position, Quaternion.identity);
                                    rangedSpawners[j].gameObject.GetComponent<SpawnerScript>().spawnedEnemy = spawnedEnemy.GetComponent<RangedEnemy>();
                                }
                            }
                        }
                        else
                        {
                            currentlySpawnedRangedEnemies++;
                            GameObject spawnedEnemy = Instantiate(rangedEnemyPrefab, rangedSpawners[y].transform.position, Quaternion.identity);
                            rangedSpawners[y].gameObject.GetComponent<SpawnerScript>().spawnedEnemy = spawnedEnemy.GetComponent<RangedEnemy>();
                        }
                    }
                }

                else if (x < 7)
                {
                    Debug.Log("chance to spawn ranged enemy: " + x + "/10, spawning one ranged enemy");

                    int y = UnityEngine.Random.Range(0, rangedSpawners.Length);

                    if (rangedSpawners[y].gameObject.GetComponent<SpawnerScript>().spawnedEnemy != null)
                    {
                        return;
                    }
                    else
                    {
                        currentlySpawnedRangedEnemies++;
                        GameObject spawnedEnemy = Instantiate(rangedEnemyPrefab, rangedSpawners[y].transform.position, Quaternion.identity);
                        rangedSpawners[y].gameObject.GetComponent<SpawnerScript>().spawnedEnemy = spawnedEnemy.GetComponent<RangedEnemy>();
                    }
                }

                else if (x >= 8)
                {
                    Debug.Log("chance to spawn ranged enemy: " + x + "/10, not spawning ranged enemies");
                }
            }
            else
            {
                Debug.Log("Condidions not met, not spawning ranged enemies");
            }

            if (amountOfMeeleEnemiesToSpawn > 0 && meeleEnemyPrefab != null)
            {
                if (SpawnBoss())
                {
                    amountOfMeeleEnemiesToSpawn--;
                    Debug.Log("spawning: " + amountOfMeeleEnemiesToSpawn + " meele enemies" + " plus a boss");
                    for (int i = 0; i < amountOfMeeleEnemiesToSpawn; i++)
                    {
                        currentlySpawnedMeeleEnemies++;
                        int x = UnityEngine.Random.Range(0, meeleSpawners.Length);
                        Instantiate(meeleEnemyPrefab, meeleSpawners[x].transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    Debug.Log("boss chance failed, spawning: " + amountOfMeeleEnemiesToSpawn + " meele enemies");
                    for (int i = 0; i < amountOfMeeleEnemiesToSpawn; i++)
                    {
                        currentlySpawnedMeeleEnemies++;
                        int x = UnityEngine.Random.Range(0, meeleSpawners.Length);
                        Instantiate(meeleEnemyPrefab, meeleSpawners[x].transform.position, Quaternion.identity);
                    }
                }

            }
            else
            {
                Debug.Log("Amount of meele enemies is full, not spawning");
            }
        }

        Debug.Log("spawned " + currentlySpawnedMeeleEnemies + " meele enemies + " + currentlySpawnedRangedEnemies + " ranged enemies");

        /*
                if (meeleEnemies.Length < maxMeleeEnemies && meeleEnemies.Length < quantityOfKillsToWin && meeleEnemyPrefab != null)
                {
                    int x = UnityEngine.Random.Range(0, meeleSpawners.Length);
                    Instantiate(meeleEnemyPrefab, meeleSpawners[x].transform.position, Quaternion.identity);
                }

                if (rangedEnemies.Length < rangedSpawners.Length && (meeleEnemies.Length + rangedEnemies.Length < quantityOfKillsToWin) && rangedEnemyPrefab != null)
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

                */
    }

    public void InvokeSpawningEnemies(float enemyRespawnRate)
    {
        if (GameManager.Instance.IsGameActive())
        {
            quanityOfKillsThisRound = GameManager.Instance.ReturnQuantityOfKillsToWin();
            InvokeRepeating("SpawnEnemies", 2, enemyRespawnRate);

        }
    }

    public void CancelInvokeSpawningEnemies()
    {
        CancelInvoke("SpawnEnemies");
    }

    private void UpdateEnemyCount()
    {
        meeleEnemiesDebug = GameObject.FindObjectsOfType<MeeleEnemyScript>();
        rangedEnemiesDebug = GameObject.FindObjectsOfType<RangedEnemy>();
    }

    private void ChangeBossChance()
    {
        if (GameManager.Instance.ReturnQuantityOfKillsToWin() <= quanityOfKillsThisRound * 0.7f)
        {
            currentBossChance = chanceToSpawnBoss[1] * bossSpawnChanceRoundMultiplier[(int)currentRoundMultiplier];
        }
        else if (GameManager.Instance.ReturnQuantityOfKillsToWin() <= quanityOfKillsThisRound * 0.5f)
        {
            currentBossChance = chanceToSpawnBoss[2] * bossSpawnChanceRoundMultiplier[(int)currentRoundMultiplier];
        }
        else if (GameManager.Instance.ReturnQuantityOfKillsToWin() <= quanityOfKillsThisRound * 0.3f)
        {
            currentBossChance = chanceToSpawnBoss[3] * bossSpawnChanceRoundMultiplier[(int)currentRoundMultiplier];
        }
    }

    private bool SpawnBoss()
    {
        float randValue = UnityEngine.Random.value;
        Debug.Log(randValue);

        if (randValue <= currentBossChance)
        {
            int x = UnityEngine.Random.Range(0, meeleSpawners.Length);
            Instantiate(bossPrefab, meeleSpawners[x].transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }
}
