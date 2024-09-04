using System;
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


    public event EventHandler onSpecialAttack;

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

        GameInput.Instance.OnSpecialMovePerformed += GameInput_OnSpecialMovePerformed;

    }

    private void GameInput_OnSpecialMovePerformed(object sender, EventArgs e)
    {
        if (ReturnCanActivateSpecial())
        {
            ResetSpecialMeter();
            onSpecialAttack?.Invoke(this, EventArgs.Empty);
        }
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

    public void ReduceSpecialMeter(float amount)
    {
        float value = maxSliderValue * (amount / 100);
        maxSliderValue -= value;
    }
}
