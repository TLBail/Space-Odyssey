using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SondageOperation : MonoBehaviour
{

    [SerializeField]
    private GameObject sondageView;


    private void Start()
    {
        sondageView.SetActive(false);
    }

    public void onSonderClick()
    {
        sondageView.SetActive(!sondageView.activeSelf);
    }

}
