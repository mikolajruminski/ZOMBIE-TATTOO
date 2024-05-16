using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private BaseEnemyAI enemyAiScript;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAiScript = GetComponent<BaseEnemyAI>();
        enemyAiScript.OnAttackPerformed += OnAttackPerformed;
    }

    private void OnAttackPerformed(object sender, EventArgs e)
    {
        animator.SetTrigger("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
