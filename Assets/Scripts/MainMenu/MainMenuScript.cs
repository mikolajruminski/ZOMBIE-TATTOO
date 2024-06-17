using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDescriptionPanel, basicMenuButtons, levelSelectionButtons;
    [SerializeField] private TextMeshProUGUI levelName, levelDescription, enemiesInTheLevel, NumberOfRounds;
    [SerializeField] private Image levelSprite;
    private MenuLevelSO currentChosenLevel;
    // Start is called before the first frame update
    void Start()
    {
        levelDescriptionPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void UpdateDescriptionPanel(MenuLevelSO menuLevelSO)
    {
        levelName.text = menuLevelSO.menuSceneName;
        levelDescription.text = menuLevelSO.menuSceneDescription;
        enemiesInTheLevel.text = "Enemies: " + menuLevelSO.enemiesInTheLevel;
        NumberOfRounds.text = "Number of rounds: " + menuLevelSO.numberOfRounds;
        levelSprite.sprite = menuLevelSO.levelImage;
        currentChosenLevel = menuLevelSO;
    }

    public void OnStartButtonClick()
    {
        basicMenuButtons.gameObject.SetActive(false);
        levelSelectionButtons.gameObject.SetActive(true);
        levelDescriptionPanel.gameObject.SetActive(true);
        FirstLevelUpdateDescription();
    }

    public void FirstLevelUpdateDescription()
    {
        MenuLevelListScript menuLevelListScript = GetComponentInChildren<MenuLevelListScript>();

        UpdateDescriptionPanel(menuLevelListScript.GetMenuLevelSOs());
    }

    public void StartTheLevel() 
    {
        SceneManager.LoadScene(currentChosenLevel.unitySceneIndex);
    }

}
