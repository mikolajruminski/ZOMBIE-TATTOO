using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusAligements : MonoBehaviour
{
    private bool onFire;
   // private Aliments currentAliment;
    private int damageTicks;
    private EnemyScript enemy;
    private EnemyAlimentsVisualScript alimentVisual;
    [SerializeField] private float timeBetweenTicks = 2f;
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
        onFire, Toxic
    }

    public void GiveAliment(int fireDamageTicks)
    {
        onFire = true;
        damageTicks = fireDamageTicks;

        StartCoroutine(burnEnemy(damageTicks));
    }

    private IEnumerator burnEnemy(int ticks)
    {
        while (ticks >= 0)
        {
            ticks--;
            alimentVisual.emitParticles();

            enemy.TakeDamage(1);

            yield return new WaitForSeconds(timeBetweenTicks);
        }

        alimentVisual.StopEmittingParticles();
        onFire = false;

    }

}
