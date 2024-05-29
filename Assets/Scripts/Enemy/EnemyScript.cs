using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int speed;
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.AddEnemyKills();
            SpecialMeter.Instance.FillSpecialMeter(GetComponent<BaseEnemyAI>().GetPointValue());
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetSpeed()
    {
        return speed;
    }
}
