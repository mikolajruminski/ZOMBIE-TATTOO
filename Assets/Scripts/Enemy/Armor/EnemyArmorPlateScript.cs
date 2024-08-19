using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmorPlateScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int plateDurability;
    public void TakeDamage(int damage)
    {
        plateDurability -= damage;

        if (plateDurability <= 0)
        {
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

    public void SetHeavyArmor()
    {
        plateDurability += plateDurability / 2;
    }
}
