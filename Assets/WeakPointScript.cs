using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointScript : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        EnemyScript enemyScript = GetComponentInParent<EnemyScript>();
        enemyScript.TakeDamage(damage);
        NewWeakPoint();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NewWeakPoint()
    {
        BossEnemyScript bossEnemyScript = GetComponentInParent<BossEnemyScript>();
        bossEnemyScript.NewWeakPoint();
    }

}
