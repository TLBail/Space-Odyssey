using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoqueBar : MonoBehaviour
{
    [SerializeField] private Slider  _slider;
    private void Update()
    {

        _slider.maxValue = PlayerManager.Instance.MaxVie;
        _slider.value = PlayerManager.Instance.Vie;
    }

}
