using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeScript : MonoBehaviour
{
    public static PlayerUpgradeScript Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

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


}
