using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponUpgradeSo : ScriptableObject
{
    public WeaponManagerScript.AllGuns gunAffected;
    public WeaponManagerScript.AllWeaponUpgrades weaponUpgrade;
    public WeaponManagerScript.RoundAlimentUpgrades roundAlimentUpgrade;

    public float amount;

    public int amountOfTicksForRoundsUpgrades;
    public int tickDamage;
}
