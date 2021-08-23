using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exoGroundController : MonoBehaviour
{
    [Space(20)]
    [SerializeField] private Image BackgroundSp;
    
    private void OnEnable()
    {
        BackgroundSp.sprite = GameManager.Instance.actualPlanete.groundSprite;

    }


   
}
