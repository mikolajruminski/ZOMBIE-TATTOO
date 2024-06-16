using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<InventoryItem> playerInventory = new List<InventoryItem>();

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


    public void AddItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Consumable:
                item.UseConsumable();
                Debug.Log("used: " + item.name);
                break;
            case InventoryItem.ItemType.WeaponUpgrade:

                WeaponManagerScript.Instance.GiveWeaponUpgrade(item);

                Debug.Log("added: " + item.name);
                break;

            case InventoryItem.ItemType.PlayerUpgrade:

                playerInventory.Add(item);
                PlayerUpgradeScript.Instance.AddedNewUpgrade(item.playerUpgradeSO);

                Debug.Log("added to player inventory: " + item.name);
                break;
        }
    }


}
