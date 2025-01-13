using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private BaseEnemyAI enemyAiScript;
    private Animator animator;
    private EnemyScript enemyScript;

    private MeeleEnemyScript meeleEnemyScript;
    private RangedEnemy rangedEnemyScript;
    private LimbsMissingScript limbsMissingScript;


    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (GetComponentInParent<MeeleEnemyScript>() != null)
        {
            meeleEnemyScript = GetComponentInParent<MeeleEnemyScript>();
        }

        if (GetComponentInParent<RangedEnemy>() != null)
        {
            rangedEnemyScript = GetComponentInParent<RangedEnemy>();
        }

        enemyAiScript = GetComponentInParent<BaseEnemyAI>();
        enemyScript = GetComponentInParent<EnemyScript>();
        limbsMissingScript = GetComponentInChildren<LimbsMissingScript>();

    }
    void Start()
    {
        limbsMissingScript.OnShot += OnShot;
        enemyAiScript.OnAttackPerformed += OnAttackPerformed;
        enemyScript.OnDeath += OnDeath;

        if (meeleEnemyScript != null)
        {
            meeleEnemyScript.onLostLeg += OnLostLeg;
        }
    }

    private void OnShot(object sender, LimbsMissingScript.OnShotEventArgs e)
    {

        switch (e.eventLimb)
        {
            case LimbsMissingScript.Limb.Larm:
                animator.SetTrigger("SoftSLeftArm");

                animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 1f);
                break;

            case LimbsMissingScript.Limb.Rarm:
                animator.SetTrigger("SoftSRightArm");
                animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 1f);
                break;

            case LimbsMissingScript.Limb.Else:
                animator.SetTrigger("SoftSThorax");

                animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 1f);
                break;
        }
    }

    private void OnLostLeg(object sender, EventArgs e)
    {
        animator.SetTrigger("FallLimbedEnemy");
    }

    private void OnDeath(object sender, EventArgs e)
    {
        // animator.Play("LimbEnemyDeathAnimation");
        animator.enabled = false;
        Death();
    }

    private void OnAttackPerformed(object sender, EventArgs e)
    {
        animator.SetTrigger("EnemyAttack");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeeleAttack()
    {
        meeleEnemyScript.MeeleAttack();
    }

    public void RangedAttack()
    {
        rangedEnemyScript.RangedAttack();
    }

    public void Death()
    {
        enemyScript.DestroyOnDeath();
    }

    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        if (meeleEnemyScript != null)
        {
            meeleEnemyScript.SetPosition(position);
        }
    }

    private IEnumerator DelayedAnimatorEnable()
    {
        yield return new WaitForSeconds(2);
        animator.enabled = true;
    }
}
