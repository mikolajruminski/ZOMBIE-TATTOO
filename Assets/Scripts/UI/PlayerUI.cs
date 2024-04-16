using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI currentHP;
    [SerializeField] private GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.gameIsActive += GameManager_Instance_GameIsActive;
        FotelHealthScript.Instance.onHealthChanged += FotelHealthScript_Instance_OnHealthChanged;
        gameObject.SetActive(false);

    }

    private void GameManager_Instance_GameIsActive(object sender, EventArgs e)
    {
        playerUI.gameObject.SetActive(true);
        currentHP.text = "" + FotelHealthScript.Instance.ReturnFotelHealth();
    }

    private void FotelHealthScript_Instance_OnHealthChanged(object sender, EventArgs e)
    {
        currentHP.text = "" + FotelHealthScript.Instance.ReturnFotelHealth();
    }


    private void Update()
    {
        UpdateAmmoStats();
    }

    private void UpdateAmmoStats()
    {
        if (GameManager.Instance.IsGameActive())
        {
            ammoText.text = GameManager.Instance.GetActiveGun().ReturnMagazineSize() + "/" +
            GameManager.Instance.GetActiveGun().ReturnCurrentAmmo();
        }

    }

}
