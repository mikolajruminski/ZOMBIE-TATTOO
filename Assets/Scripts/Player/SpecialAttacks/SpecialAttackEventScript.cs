using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackEventScript : MonoBehaviour
{

    public event EventHandler onForceWaveAttackAnimationEnded;

    #region  Special Attacks scripts
    private InkMachineAttackScript inkMachineAttackScript;
    private SpecialMoveScript specialMoveScript;

    #endregion

    private void Awake() 
    {
        inkMachineAttackScript = GetComponentInChildren<InkMachineAttackScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        specialMoveScript = GetComponentInChildren<SpecialMoveScript>();
        GameArmsAnimatorScript.Instance.OnForceAttackAnimationEnded += onForceAttackAnimationEnded;
        GameArmsAnimatorScript.Instance.OnFuryTimeAnimationEnded += onFuryTimeAnimationEnded;
        GameArmsAnimatorScript.Instance.onInkAttackStart += onInkAttackStart;
    }
    private void onInkAttackStart(object sender, EventArgs e)
    {
        inkMachineAttackScript.StartDealingDamage();
    }

    private void onFuryTimeAnimationEnded(object sender, EventArgs e)
    {
        PlayerUpgradeScript.Instance.OnFuryTimeAnimationEnded();
    }

    private void onForceAttackAnimationEnded(object sender, EventArgs e)
    {
        StartCoroutine(specialMoveScript.EnableAttackColldier());
    }
}
