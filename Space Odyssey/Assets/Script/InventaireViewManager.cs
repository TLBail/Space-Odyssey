using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaireViewManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonAmelioration;
    


    private void OnEnable()
    {
        if(GameManager.Instance.actualPlanete != null) buttonAmelioration.SetActive(GameManager.Instance.actualPlanete.haveASpaceStation);
        else buttonAmelioration.SetActive(false);
    }
}
