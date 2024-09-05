using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private BaseEnemyAI enemyAiScript;
    private Animator animator;
    private EnemyScript enemyScript;

    private MeeleEnemyScript meeleEnemyScript;

    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        meeleEnemyScript = GetComponentInParent<MeeleEnemyScript>();
        enemyAiScript = GetComponentInParent<BaseEnemyAI>();
        enemyScript = GetComponentInParent<EnemyScript>();
    }
    void Start()
    {
        enemyAiScript.OnAttackPerformed += OnAttackPerformed;
        enemyScript.OnNoHeadDeath += OnNoHeadDeath;
        enemyScript.OnDeath += OnDeath;
    }

    private void OnDeath(object sender, EventArgs e)
    {
        animator.Play("LimbEnemyDeathAnimation");
    }

    private void OnNoHeadDeath(object sender, EventArgs e)
    {
        animator.Play("NoHeadDeathAnimation");
    }

    private void OnAttackPerformed(object sender, EventArgs e)
    {
        animator.SetTrigger("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeeleAttack()
    {
        meeleEnemyScript.MeeleAttack();
    }

    public void Death()
    {
        enemyScript.DestroyOnDeath();
    }
}
