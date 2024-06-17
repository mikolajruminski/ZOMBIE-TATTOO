using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeScript : MonoBehaviour
{
    public static PlayerUpgradeScript Instance { get; private set; }
    private bool isFuryTimeActive;
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
            Debug.Log("activating fury time");
            isFuryTimeActive = true;
            WeaponManagerScript.Instance.ActivateFuryTimeForAllWeapons();
            FotelHealthScript.Instance.FuryTimeCanTakeDamage(false);
        }
        else
        {
            Debug.Log("cannot activate fury time");
            yield return null;
        }

        yield return new WaitForSeconds(furyTime);

        if (isFuryTimeActive)
        {
            Debug.Log("disabling fury time");
            isFuryTimeActive = false;
            WeaponManagerScript.Instance.DisableFuryTimeForAllWeapons();
            FotelHealthScript.Instance.FuryTimeCanTakeDamage(true);
        }


    }


}
