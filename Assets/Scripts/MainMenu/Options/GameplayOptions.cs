using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayOptions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown crosshairColorDropdown;

    private List<Color> coloursList = new List<Color>() { Color.black, Color.red, Color.blue, Color.green, Color.yellow, Color.white, Color.magenta };

    private void Start()
    {
        SetColors();
    }

    private void SetColors()
    {
      
        crosshairColorDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < coloursList.Count; i++)
        {
            string colorOptions = coloursList[i].ToString();
            options.Add(colorOptions);

            /*
                      if (crosshairColors[i] == player.GetCrosshairColor)
                        {
                            something happens
                        }
                        */
        }

        crosshairColorDropdown.AddOptions(options);
        crosshairColorDropdown.RefreshShownValue();

    }

    public void SetCrosshairColor(int colorIndex)
    {
        //integrate with the game
    }

}
