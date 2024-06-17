using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelListScript : MonoBehaviour
{
    [SerializeField] private List<MenuLevelSO> menuLevelSOs = new List<MenuLevelSO>();
    [SerializeField] private GameObject levelButton;
    [SerializeField] private GameObject LevelSelectionButtonsPanel;
    // Start is called before the first frame update
    void Start()
    {
        foreach (MenuLevelSO menuLevelSO in menuLevelSOs)
        {
            GameObject instantiatedLevelButton = Instantiate(levelButton, LevelSelectionButtonsPanel.transform);
            instantiatedLevelButton.GetComponent<MenuLevelButtonScript>().SetMenuLevelSO(menuLevelSO);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MenuLevelSO GetMenuLevelSOs()
    {
        return menuLevelSOs[0];
    }
}
