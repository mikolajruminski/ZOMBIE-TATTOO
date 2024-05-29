using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMeter : MonoBehaviour
{
    public static SpecialMeter Instance { get; private set; }
    [HideInInspector]
    public float specialMeter = 0;

    private bool canActivateSpecial = false;
    [SerializeField] private Slider specialSlider;
    [SerializeField] private TextMeshProUGUI specialCanBeActivatedText;
    [SerializeField] private float maxSliderValue = 100;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        specialCanBeActivatedText.gameObject.SetActive(false);
        Mathf.Clamp(specialMeter, 0, 100);
        specialSlider.maxValue = maxSliderValue;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FillSpecialMeter(float pointValue)
    {
        specialMeter += pointValue;
        specialSlider.value = specialMeter;

        if (specialMeter >= maxSliderValue)
        {
            canActivateSpecial = true;
            specialCanBeActivatedText.gameObject.SetActive(true);
        }
    }

    public void ResetSpecialMeter()
    {
        specialMeter = 0;
        specialSlider.value = specialMeter;
        canActivateSpecial = false;

        specialCanBeActivatedText.gameObject.SetActive(false);
    }

    public bool ReturnCanActivateSpecial()
    {
        return canActivateSpecial;
    }
}
