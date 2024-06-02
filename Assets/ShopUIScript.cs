using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private GameObject itemContainter;
    [SerializeField] private GameObject panelParent;
    [SerializeField] private TextMeshProUGUI playerMoneyText;

    [SerializeField] private TextMeshProUGUI descriptionPanelName;
    [SerializeField] private TextMeshProUGUI descriptionPanelDesc;
    [SerializeField] private GameObject descriptionPanelSprite;

    private InventoryItem currentlyDisplayedItem;

    [SerializeField] private bool useMoney = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMoneyText.text = "Gold: " + MoneyManager.Instance.ReturnPlayerMoney();

        MoneyManager.Instance.onMoneyChanged += MoneyManager_Instance_OnMoneyChanged;

        foreach (InventoryItem shopItem in ShopManager.Instance.allItems)
        {
            GameObject instantiated = Instantiate(itemContainter, panelParent.transform);
            instantiated.gameObject.GetComponent<ShopItemScript>().SetItemParameters(shopItem);
        }
    }

    private void MoneyManager_Instance_OnMoneyChanged(object sender, EventArgs e)
    {
        playerMoneyText.text = "Gold: " + MoneyManager.Instance.ReturnPlayerMoney();
    }

    public void UpdateDescriptionPanel(InventoryItem item)
    {
        descriptionPanelName.text = item.name;
        descriptionPanelDesc.text = item.description;
        descriptionPanelSprite.GetComponent<Image>().sprite = item.sprite;

        currentlyDisplayedItem = item;
    }

    public void BuyItem()
    {
        if (useMoney)
        {
            if (MoneyManager.Instance.ReturnPlayerMoney() >= currentlyDisplayedItem.itemPrice)
            {
                InventoryManager.Instance.AddItem(currentlyDisplayedItem);
                MoneyManager.Instance.SubtractMoney(currentlyDisplayedItem.itemPrice);
            }
            else
            {
                Debug.Log("not enough money!");
            }
        }
        else
        {
            InventoryManager.Instance.AddItem(currentlyDisplayedItem);

        }

    }
}
