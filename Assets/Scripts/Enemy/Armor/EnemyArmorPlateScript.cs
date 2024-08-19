using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmorPlateScript : MonoBehaviour, IDamageable
{
    [SerializeField] private ArmorType armorType;
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

    public void SetArmor(int durability)
    {
        this.plateDurability = durability;
    }


    public enum ArmorType
    {
        Head, Leg, Arm, Chest
    }

    public ArmorType GetArmorType()
    {
        return armorType;
    }
}
