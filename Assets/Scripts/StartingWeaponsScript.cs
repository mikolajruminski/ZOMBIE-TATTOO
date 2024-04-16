using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeaponsScript : MonoBehaviour, IUseable
{
    public string Name => "";

    public string Description => "Pick " + weaponName;
    private string weaponName;

    [SerializeField] private WeaponManagerScript.AllGuns gun;

    private void Awake()
    {
        weaponName = gun.ToString();
    }

    public void Interact()
    {
        WeaponManagerScript.Instance.SetStartingWeapon(gun);
    }

}
