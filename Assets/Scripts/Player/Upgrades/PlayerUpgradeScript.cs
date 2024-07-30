using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeScript : MonoBehaviour
{
    public static PlayerUpgradeScript Instance { get; private set; }
    private bool isFuryTimeActive;

    public event EventHandler<OnFuryTimeActivatedEventArgs> onFuryTimeActivated;

    public class OnFuryTimeActivatedEventArgs
    {
        public float furyTime;
    }

    private float furyTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        isFuryTimeActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum PlayerUpgrades
    {
        HealthUpgrade, EarnMoreSpecialPoints, FuryTime
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

    public IEnumerator ActivateFuryTime(float furyTime)
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
        onFuryTimeActivated?.Invoke(this, new OnFuryTimeActivatedEventArgs { furyTime = furyTime });
    }


}
