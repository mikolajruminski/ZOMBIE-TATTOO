using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImmunitiesScript : MonoBehaviour
{
    private EnemyStatusAligements enemyStatusAligements;
    [SerializeField] private EnemyStatusAligements.Aliments immunityForAliment;

    private Immunities[] immunities;

    private Dictionary<Immunities, float> ImmunitiesDictionary = new Dictionary<Immunities, float>();

    private float lightArmorDamageReduction = 50f;
    private float heavyArmorDamageReduction = 70f;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatusAligements = GetComponent<EnemyStatusAligements>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public enum Immunities
    {
        None, LightArmor, HeavyArmor, Fire, Toxic, Push,
    }

    private void FillImmunitiesDictionary()
    {
        ImmunitiesDictionary.Add(Immunities.LightArmor, lightArmorDamageReduction);
        ImmunitiesDictionary.Add(Immunities.HeavyArmor, heavyArmorDamageReduction);
    }

    public void IsImmune(WeaponManagerScript.AllWeaponUpgrades alimentUpgrade)
    {
        for (int i = 0; i < immunities.Length; i++)
        {
            
       }
    }
}
