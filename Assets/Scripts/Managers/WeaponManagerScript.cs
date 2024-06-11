using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    public static WeaponManagerScript Instance { get; private set; }
    [SerializeField] private GunSystem[] guns;
    public event EventHandler onGunChanged;
    private List<KeyCode> keys = new List<KeyCode>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (GunSystem gun in guns)
        {
            keys.Add(gun.weaponKey);
        }
    }

    public void Update()
    {
        ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                foreach (GunSystem gun in guns)
                {
                    if (gun.weaponKey == key)
                    {
                        gun.gameObject.SetActive(true);
                        GameManager.Instance.SetActiveGun(gun);
                        onGunChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        gun.gameObject.SetActive(false);
                    }
                }
            }
        }

    }


    public void SetStartingWeapon()
    {
        foreach (GunSystem gun in guns)
        {
            if (gun.gameObject.activeInHierarchy)
            {
                GameManager.Instance.SetActiveGun(gun);
            }
        }
    }

    public void GiveWeaponUpgrade(InventoryItem item)
    {
        foreach (GunSystem gun in guns)
        {
            if (gun.gunType == item.weaponUpgradeSO.gunAffected)
            {
                gun.gameObject.GetComponent<WeaponUpgradeSystem>().AddWeaponUpgrades(item);
            }
        }
    }


    public enum AllGuns
    {
        Pistol, Shotgun
    }

    public enum AllWeaponUpgrades
    {
        None, MagazienSizeIncraese, FireRateIncrease, ReloadSpeedDecrease, DamageIncrease, fireRounds, toxicRounds, normalRounds
    }


}
