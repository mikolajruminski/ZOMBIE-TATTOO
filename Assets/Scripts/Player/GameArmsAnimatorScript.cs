using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArmsAnimatorScript : MonoBehaviour
{
    public static GameArmsAnimatorScript Instance { get; private set; }
    public event EventHandler OnFuryTimeAnimationEnded;
    public event EventHandler OnSpecialAttackAnimationEnded;
    public event EventHandler onInkAttackStart;
    Animator animator;
    // Start is called before the first frame update

    private float furyTime;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        PlayerUpgradeScript.Instance.onFuryTimeActivated += OnFuryTimeActivated;
        PlayerUpgradeScript.Instance.onForceWaveAttackActivated += OnForceWaveAttackActivated;
        PlayerUpgradeScript.Instance.onFTattoInkAttackActivated += OnFTattoInkAttackActivated;
        //animator.enabled = false;
    }

    private void OnFTattoInkAttackActivated(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("TattoInkAttackAct");
    }

    private void OnForceWaveAttackActivated(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("ForceWaveAttack");
    }

    private void OnFuryTimeActivated(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("FuryTimeActivation");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SPECIAL_ATTACK()
    {
        Debug.Log("reached game arms script");
        OnSpecialAttackAnimationEnded?.Invoke(this, EventArgs.Empty);
    }

    public void ActivateFuryTime()
    {
        OnFuryTimeAnimationEnded?.Invoke(this, EventArgs.Empty);
    }

    public void InkAttackStart()
    {
        onInkAttackStart?.Invoke(this, EventArgs.Empty);
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
