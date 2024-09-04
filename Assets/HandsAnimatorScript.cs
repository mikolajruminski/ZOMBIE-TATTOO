using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimatorScript : MonoBehaviour
{
    private const string BENELLI_HAND_PLACEMENT = "BenelliHandPlacement";
    private const string PISTOL_HAND_PLACEMENT = "PistolHandPlacement";
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        WeaponManagerScript.Instance.onGunChanged += OnGunChanged;
    }

    private void OnGunChanged(object sender, EventArgs e)
    {
        switch (GameManager.Instance.GetActiveGun().ReturnGunType())
        {
            case WeaponManagerScript.AllGuns.Pistol:
                animator.Play(PISTOL_HAND_PLACEMENT);

                break;

            case WeaponManagerScript.AllGuns.Shotgun:
                animator.Play(BENELLI_HAND_PLACEMENT);

                break;
        }
    }
}
