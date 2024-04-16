using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    public static WeaponManagerScript Instance { get; private set; }
    [SerializeField] private GunSystem[] guns;

    private bool isStartingWeaponChosen = false;
    private void Awake()
    {
        Instance = this;
    }

    public void SetStartingWeapon(AllGuns allGuns)
    {
        isStartingWeaponChosen = true;
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].gunType == allGuns)
            {
                guns[i].gameObject.SetActive(true);
                GameManager.Instance.SetActiveGun(guns[i]);
            }
            else
            {
                guns[i].gameObject.SetActive(false);
            }
        }


    }

    public enum AllGuns
    {
        Pistol, Shotgun
    }

    public bool IsStartingWeaponChosen()
    {
        return isStartingWeaponChosen;
    }

}
