using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FuelBar : MonoBehaviour
{
    [SerializeField] private Slider  _slider;
    private void Update()
    {

        _slider.maxValue = PlayerManager.Instance.MaxEnergie;
        _slider.value = PlayerManager.Instance.Energie;
    }
    
}
