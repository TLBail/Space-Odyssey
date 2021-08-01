using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaireViewManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonAmelioration;

    public bool onSpaceStation;



    private void OnEnable()
    {
        if (onSpaceStation)
        {
            buttonAmelioration.SetActive(true);
        }
        else
        {
            buttonAmelioration.SetActive(false);
        }
    }
}
