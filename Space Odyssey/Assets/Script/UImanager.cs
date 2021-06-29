using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImanager : MonoBehaviour
{
    [SerializeField] private Slider _sliderVie, _sliderEnergie;

    private void Update()
    {
        _sliderEnergie.maxValue = PlayerManager.Instance.MaxEnergie;
        _sliderVie.maxValue = PlayerManager.Instance.MaxVie;
        _sliderEnergie.value = PlayerManager.Instance.Energie;
        _sliderVie.value = PlayerManager.Instance.Vie;
    }
}
