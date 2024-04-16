using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler gameIsActive;
    [SerializeField] private GameObject gamePlayer, preGamePlayer;
    private bool isGameActive = false;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyRespawnRate;

    [SerializeField] int maxEnemies = 5;

    private GunSystem activeGun;

    private void Awake()
    {
        gamePlayer.SetActive(false);
        Instance = this;
    }
    public void SwitchGameMode()
    {
        if (WeaponManagerScript.Instance.IsStartingWeaponChosen())
        {
            preGamePlayer.SetActive(false);
            gamePlayer.SetActive(true);
            isGameActive = true;
            InvokeSpawningEnemies();

            gameIsActive?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("pick starting weapon!");
        }

    }

    public void GameOver()
    {
        Destroy(PlayerController.Instance);
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
        EnemyAI[] enemies = GameObject.FindObjectsOfType<EnemyAI>();

        if (enemies.Length < maxEnemies)
        {
            int x = UnityEngine.Random.Range(0, spawners.Length);
            Instantiate(enemyPrefab, spawners[x].transform.position, Quaternion.identity);
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

}
