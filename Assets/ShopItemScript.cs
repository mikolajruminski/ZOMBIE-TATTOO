using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private GameObject itemSprite;
    [SerializeField] private TextMeshProUGUI itemPrice;
    private InventoryItem item;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetItemParameters(InventoryItem item)
    {
        itemName.text = item.name;
        itemSprite.GetComponent<Image>().sprite = item.sprite;
        itemPrice.text = item.itemPrice.ToString() + "G";
        this.item = item;
    }

    public void UpdateDescriptionPanel()
    {
        GetComponentInParent<ShopUIScript>().UpdateDescriptionPanel(item);
    }


}
