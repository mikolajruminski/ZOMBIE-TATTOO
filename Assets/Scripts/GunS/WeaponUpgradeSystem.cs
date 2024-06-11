using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeSystem : MonoBehaviour
{
    private List<InventoryItem> weaponUpgrades = new List<InventoryItem>();
    public WeaponManagerScript.AllWeaponUpgrades roundsUpgrade = new WeaponManagerScript.AllWeaponUpgrades();
    private GunSystem gun;
    private int alimentDamageTicks, alimentDamage;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<GunSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddWeaponUpgrades(InventoryItem weaponUpgrade)
    {
        weaponUpgrades.Add(weaponUpgrade);

        SetUpgradeParameters(weaponUpgrade.weaponUpgradeSO);

    }

    private void SetUpgradeParameters(WeaponUpgradeSo weaponUpgradeSo)
    {
        float value = 0;
        switch (weaponUpgradeSo.weaponUpgrade)
        {
            case WeaponManagerScript.AllWeaponUpgrades.DamageIncrease:
                value = gun.ReturnUpgradableParameters()[0];
                Debug.Log("Initial before upgrade: " + value);
                value += weaponUpgradeSo.amount;

                gun.ReturnUpgradableParameters()[0] = value;

                Debug.Log("Gun parameter: " + gun.ReturnUpgradableParameters()[0]);

                gun.UpgradeUpgradableParameters();
                break;
            case WeaponManagerScript.AllWeaponUpgrades.FireRateIncrease:
                value = gun.ReturnUpgradableParameters()[1];
                Debug.Log("Initial before upgrade: " + value);

                value *= (weaponUpgradeSo.amount / 100);

                gun.ReturnUpgradableParameters()[1] -= value;

                Debug.Log("Gun parameter: " + gun.ReturnUpgradableParameters()[1]);

                gun.UpgradeUpgradableParameters();
                break;
            case WeaponManagerScript.AllWeaponUpgrades.ReloadSpeedDecrease:
                value = gun.ReturnUpgradableParameters()[2];
                Debug.Log("Initial before upgrade: " + value);

                value *= (weaponUpgradeSo.amount / 100);

                gun.ReturnUpgradableParameters()[2] -= value;
                Debug.Log("Gun parameter: " + gun.ReturnUpgradableParameters()[2]);

                gun.UpgradeUpgradableParameters();
                break;
            case WeaponManagerScript.AllWeaponUpgrades.MagazienSizeIncraese:
                value = gun.ReturnUpgradableParameters()[3];

                Debug.Log("Initial before upgrade: " + value);

                value += weaponUpgradeSo.amount;

                gun.ReturnUpgradableParameters()[3] = value;

                Debug.Log("Gun parameter: " + gun.ReturnUpgradableParameters()[3]);

                gun.UpgradeUpgradableParameters();
                break;
            case WeaponManagerScript.AllWeaponUpgrades.fireRounds:

                roundsUpgrade = WeaponManagerScript.AllWeaponUpgrades.fireRounds;

                alimentDamageTicks = weaponUpgradeSo.amountOfTicksForRoundsUpgrades;
                alimentDamage = weaponUpgradeSo.tickDamage;

                break;
            case WeaponManagerScript.AllWeaponUpgrades.toxicRounds:

                roundsUpgrade = WeaponManagerScript.AllWeaponUpgrades.toxicRounds;

                alimentDamageTicks = weaponUpgradeSo.amountOfTicksForRoundsUpgrades;
                alimentDamage = weaponUpgradeSo.tickDamage;

                break;
        }
    }

    public WeaponManagerScript.AllWeaponUpgrades HasAliment()
    {
        return roundsUpgrade;
    }

    public int ReturnTickAmount()
    {
        return alimentDamageTicks;
    }

    public int ReturnTickDamage()
    {
        return alimentDamage;
    }
}
