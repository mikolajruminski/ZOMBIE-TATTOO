using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimatorScript : MonoBehaviour
{
    private Animator animator;
    private GunSystem gunSystem;

    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunSystem = GetComponentInParent<GunSystem>();
    }
    void Start()
    {
        gunSystem.OnGunReload += OnGunReload;
        gunSystem.OnGunShoot += OnGunShoot;
    }

    private void OnGunShoot(object sender, EventArgs e)
    {
        animator.SetTrigger("OnShoot");
    }

    private void OnGunReload(object sender, EventArgs e)
    {
        animator.SetTrigger("OnReload");
    }

    #region Animator Ready to shoot
    public void NotReadyToShoot()
    {
        gunSystem.NotReadyToShoot();
    }

    public void ReadyToShoot()
    {
        gunSystem.ReadyToShoot();
    }

    #endregion
}
