using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeScript : MonoBehaviour
{
    public static PlayerUpgradeScript Instance { get; private set; }
    private bool isFuryTimeActive;

    public event EventHandler onFuryTimeActivated;
    private float furyTime;

    [SerializeField] private PlayerSpecialMoves chosenSpecialMove;

    #region SpecialMovesStart Events

    public event EventHandler onForceWaveAttackActivated;
    public event EventHandler onFTattoInkAttackActivated;

    #endregion

    #region  SpecialMovesAnimationEnds Events

    public event EventHandler onForceWaveAttackAnimationEnded;

    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        chosenSpecialMove = PlayerSpecialMoves.ForceWaveAttack;
    }

    void Start()
    {
        isFuryTimeActive = false;
        PlayerController.Instance.onSpecialAttack += OnSpecialAttack;
    }


    private void OnSpecialAttack(object sender, EventArgs e)
    {
        switch (chosenSpecialMove)
        {
            case PlayerSpecialMoves.ForceWaveAttack:
                onForceWaveAttackActivated?.Invoke(this, EventArgs.Empty);
                break;

            case PlayerSpecialMoves.TattoInAttack:
                onFTattoInkAttackActivated?.Invoke(this, EventArgs.Empty);
                break;
        }
    }
    public void OnFuryTimeAnimationEnded()
    {
        StartCoroutine(ActivateFuryTime());
    }

    public void OnForceWaveAttackAnimationEnded()
    {
        onForceWaveAttackAnimationEnded?.Invoke(this, EventArgs.Empty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum PlayerUpgrades
    {
        HealthUpgrade, EarnMoreSpecialPoints, FuryTime
    }

    public enum PlayerSpecialMoves
    {
        TattoInAttack, ForceWaveAttack
    }

    public void AddedNewUpgrade(PlayerUpgradeSo item)
    {
        switch (item.playerUpgrade)
        {
            case PlayerUpgrades.HealthUpgrade:
                FotelHealthScript.Instance.IncreaseFotelHealth(item.amountOfUpgrade);
                break;

            case PlayerUpgrades.EarnMoreSpecialPoints:
                SpecialMeter.Instance.ReduceSpecialMeter(item.amountOfUpgrade);
                break;
        }
    }

    public IEnumerator ActivateFuryTime()
    {
        if (!isFuryTimeActive)
        {
            isFuryTimeActive = true;
            WeaponManagerScript.Instance.ActivateFuryTimeForAllWeapons();
            FotelHealthScript.Instance.FuryTimeCanTakeDamage(false);
        }
        else
        {
            yield return null;
        }

        yield return new WaitForSeconds(furyTime);

        if (isFuryTimeActive)
        {
            isFuryTimeActive = false;
            WeaponManagerScript.Instance.DisableFuryTimeForAllWeapons();
            FotelHealthScript.Instance.FuryTimeCanTakeDamage(true);
        }
    }

    public void ConsumedFuryTime(float furyTime)
    {
        this.furyTime = furyTime;
        onFuryTimeActivated?.Invoke(this, EventArgs.Empty);
    }


}
