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
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyScript>();
        alimentVisual = GetComponent<EnemyAlimentsVisualScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum Aliments
    {
        None, onFire, Toxic
    }

    public void GiveAliment(int ticks, int damage, WeaponManagerScript.AllWeaponUpgrades alimentUpgrade)
    {
        damageTicks = ticks;

        alimentDamage = damage;

        switch (alimentUpgrade)
        {
            case WeaponManagerScript.AllWeaponUpgrades.fireRounds:
                currentAliment = Aliments.onFire;
                break;

            case WeaponManagerScript.AllWeaponUpgrades.toxicRounds:
                currentAliment = Aliments.Toxic;
                break;
        }

        StartCoroutine(alimentEnemy());
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
