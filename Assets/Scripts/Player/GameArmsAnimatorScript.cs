using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArmsAnimatorScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update

    private float furyTime;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        PlayerController.Instance.onSpecialAttack += OnSpecialAttack;
        PlayerUpgradeScript.Instance.onFuryTimeActivated += OnFuryTimeActivated;
        //animator.enabled = false;
    }

    private void OnFuryTimeActivated(object sender, PlayerUpgradeScript.OnFuryTimeActivatedEventArgs e)
    {
        animator.enabled = true;
        furyTime = e.furyTime;
        animator.Play("FuryTimeActivation");
    }

    private void OnSpecialAttack(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("SpecialAttack");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SPECIAL_ATTACK()
    {
        GetComponentInChildren<SpecialMoveScript>().SpecialAttack();
    }

    public void ActivateFuryTime()
    {
        StartCoroutine(PlayerUpgradeScript.Instance.ActivateFuryTime(furyTime));
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
