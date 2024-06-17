using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuLevelButtonScript : MonoBehaviour
{
    private MenuLevelSO menuLevelSO;
    private TextMeshProUGUI levelNameOnButton;
    private MainMenuScript mainMenuScript;
    // Start is called before the first frame update

    private void Awake()
    {
        levelNameOnButton = GetComponentInChildren<TextMeshProUGUI>();
        mainMenuScript = GetComponentInParent<MainMenuScript>();
    }
    void Start()
    {
        levelNameOnButton.text = menuLevelSO.menuSceneName;
    }

    public void OnClick()
    {
        mainMenuScript.UpdateDescriptionPanel(menuLevelSO);
    }

    public void SetMenuLevelSO(MenuLevelSO menuLevelSO)
    {
        this.menuLevelSO = menuLevelSO;
    }
}
