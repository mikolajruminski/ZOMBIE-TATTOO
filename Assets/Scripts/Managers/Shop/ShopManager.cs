using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField] public InventoryItem[] allItems;
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




    public enum ShopItems
    {
        Health_Pack, Ammo_Pack
    }
}
