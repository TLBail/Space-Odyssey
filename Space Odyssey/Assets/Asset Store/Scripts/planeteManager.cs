using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planeteManager : MonoBehaviour
{
    [SerializeField] private Image _imageBackground;
    [SerializeField] private GameObject planeteView;
    [SerializeField] private GameObject geanteView;
    
    private void OnEnable()
    {

        planeteOrbit planeteOrbit = GameManager.Instance.ActualPlanete;
        
        _imageBackground.sprite = planeteOrbit.planeteSprite;
        
        planeteView.SetActive(planeteOrbit.TypePlanete == TypePlanete.DESERTE);
        geanteView.SetActive(planeteOrbit.TypePlanete == TypePlanete.GAZEUSE);
        



    }

}
