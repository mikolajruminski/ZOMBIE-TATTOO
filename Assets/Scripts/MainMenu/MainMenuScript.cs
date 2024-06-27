using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDescriptionPanel, basicMenuButtons, levelSelectionButtons, options, optionsPanel;
    [SerializeField] private List<GameObject> panels = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI levelName, levelDescription, enemiesInTheLevel, NumberOfRounds, optionsPanelName;
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
        // UnityEditor.EditorApplication.isPlaying = false;
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

    public void OnOptionsButtonClick()
    {
        ResetOptionsPanel();

        basicMenuButtons.gameObject.SetActive(false);
        options.gameObject.SetActive(true);

        OnVideoButtonClick();
    }

    public void OnStartGameBackButtonClick()
    {
        basicMenuButtons.gameObject.SetActive(true);
        levelSelectionButtons.gameObject.SetActive(false);
        levelDescriptionPanel.gameObject.SetActive(false);
    }

    public void OnLoadGameBackButtonClick()
    {

        basicMenuButtons.gameObject.SetActive(true);
    }

    public void OnOptionsGameBackButtonClick()
    {
        options.gameObject.SetActive(false);
        basicMenuButtons.gameObject.SetActive(true);

    }

    public void OnVideoButtonClick()
    {
        ResetOptionsPanel();
        optionsPanelName.text = "Video";
        panels[0].gameObject.SetActive(true);
    }

    public void OnAudioButtonClick()
    {
        ResetOptionsPanel();

        optionsPanelName.text = "Audio";
        panels[1].gameObject.SetActive(true);
    }

    public void OnGameplayButtonClick()
    {
        ResetOptionsPanel();

        optionsPanelName.text = "Gameplay";
        panels[2].gameObject.SetActive(true);
    }

    public void OnControlsButtonClick()
    {
        ResetOptionsPanel();

        optionsPanelName.text = "Controls";
        panels[3].gameObject.SetActive(true);
    }

    private void ResetOptionsPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.gameObject.SetActive(false);
        }
    }


}
