using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resolutions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown, graphicsDropdown;
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        ResolutionOptions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void ResolutionOptions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();



        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolutionOption);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;

        resolutionDropdown.RefreshShownValue();
    }

    public void FullScreenOptions(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
