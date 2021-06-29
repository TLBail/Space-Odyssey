using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exoGroundController : MonoBehaviour
{
    [SerializeField] private Sprite[] _backGroundArray;

    [Space(20)]
    [SerializeField] private Image BackgroundSp;
    
    private void OnEnable()
    {
        BackgroundSp.sprite = _backGroundArray[GameManager.Instance.ActualPlaneteIndex];

    }


   
}
