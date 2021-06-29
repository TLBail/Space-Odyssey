using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemSelector : MonoBehaviour
{
    [SerializeField] private GameObject SliderObj, TextObj;
    private Slider _sliderCmp;
    private TextMeshProUGUI _textCmp;

    public int MaxSystemNb;

    [SerializeField] private StarPath _starPath;
    
    [TextArea] 
    [SerializeField] private String textSystem;
    private void Start()
    {

        _sliderCmp = SliderObj.GetComponent<Slider>();
        _textCmp = TextObj.GetComponent<TextMeshProUGUI>();
        _sliderCmp.maxValue = MaxSystemNb;
    }

    public void OnValueOfTheSliderChanged()
    {
        int value = (int)_sliderCmp.value;
        _textCmp.text = textSystem + value;
        _starPath._numberOfSystem = value;
    }
}
