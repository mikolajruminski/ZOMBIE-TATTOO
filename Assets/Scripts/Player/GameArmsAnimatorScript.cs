using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArmsAnimatorScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.Instance.onSpecialAttack += OnSpecialAttack;
        animator.enabled = false;
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
    
    private void DisableAnimator() 
    {
        animator.enabled = false;
    }
}
