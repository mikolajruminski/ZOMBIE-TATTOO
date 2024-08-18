using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusAligements : MonoBehaviour
{
    private Aliments currentAliment;
    private int damageTicks;
    private int alimentDamage;
    private EnemyScript enemy;
    private EnemyAlimentsVisualScript alimentVisual;
    private float timeBetweenTicks = 2f;


    private EnemyImmunitiesScript enemyImmunitiesScript;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyScript>();
        alimentVisual = GetComponent<EnemyAlimentsVisualScript>();
        enemyImmunitiesScript = GetComponent<EnemyImmunitiesScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum Aliments
    {
        None, onFire, Toxic
    }

    public void GiveAliment(int ticks, int damage, WeaponManagerScript.RoundAlimentUpgrades alimentUpgrade)
    {
        damageTicks = ticks;

        alimentDamage = damage;
        if (!enemyImmunitiesScript.IsImmune(alimentUpgrade))
        {
            switch (alimentUpgrade)
            {
                case WeaponManagerScript.RoundAlimentUpgrades.fireRounds:
                    currentAliment = Aliments.onFire;
                    break;

                case WeaponManagerScript.RoundAlimentUpgrades.toxicRounds:
                    currentAliment = Aliments.Toxic;
                    break;
            }

            StartCoroutine(alimentEnemy());
        }
        else
        {
            Debug.Log("cannot give aliment, enemy is immune to this type of damage!");
        }



    }

    private IEnumerator alimentEnemy()
    {
        EmitAlimentParticles();

        while (damageTicks >= 0)
        {
            damageTicks--;

            enemy.TakeDamage(alimentDamage);

            yield return new WaitForSeconds(timeBetweenTicks);
        }

        alimentVisual.StopEmittingParticles();
        currentAliment = Aliments.None;
    }

    private void EmitAlimentParticles()
    {
        switch (currentAliment)
        {
            case Aliments.onFire:
                alimentVisual.emitParticles(0);
                break;

            case Aliments.Toxic:
                alimentVisual.emitParticles(1);
                break;
        }
    }

}
