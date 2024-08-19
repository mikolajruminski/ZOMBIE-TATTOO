using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImmunitiesScript : MonoBehaviour
{
    private EnemyStatusAligements enemyStatusAligements;
    [SerializeField] private WeaponManagerScript.RoundAlimentUpgrades immunityForAliment;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatusAligements = GetComponent<EnemyStatusAligements>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public enum Armor
    {
        None, LightArmor, HeavyArmor,
    }

    public bool IsImmune(WeaponManagerScript.RoundAlimentUpgrades roundAliment)
    {

        if (roundAliment == immunityForAliment)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
