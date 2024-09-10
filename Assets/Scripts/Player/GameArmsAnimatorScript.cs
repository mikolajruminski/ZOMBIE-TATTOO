using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArmsAnimatorScript : MonoBehaviour
{
    public static GameArmsAnimatorScript Instance { get; private set; }
    public event EventHandler OnFuryTimeAnimationEnded;
    public event EventHandler OnForceAttackAnimationEnded;
    public event EventHandler onInkAttackStart;
    public event EventHandler OnConsumableAnimationEnded;
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
        PlayerUpgradeScript.Instance.onTattoInkAttackActivated += OnTattoInkAttackActivated;
        InkMachineAttackScript.Instance.onInkAttackEnded += OnInkAttackEnded;
        ConsumableHolderScript.Instance.OnConsumableShot += OnConsumableShot;
        //animator.enabled = false;
    }

    private void OnConsumableShot(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("CatchConsumable");
    }

    private void OnInkAttackEnded(object sender, EventArgs e)
    {
        animator.enabled = true;
        animator.Play("TattooInkAttackEnd");
    }

    private void OnTattoInkAttackActivated(object sender, EventArgs e)
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

    public void SPECIAL_ATTACK()
    {
        //needs rebuilding, events does not fire so this is an temporary solution
        OnForceAttackAnimationEnded?.Invoke(this, EventArgs.Empty);
        // PlayerUpgradeScript.Instance.OnForceWaveAttackAnimationEnded();
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

    public void UseConsumable()
    {
        OnConsumableAnimationEnded?.Invoke(this, EventArgs.Empty);
    }
}
