using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SondageOperation : MonoBehaviour
{

    [SerializeField]
    private GameObject sondageView;

    [SerializeField]
    private Item[] ressourceminable;

    private PlayerManager playerManager;

    public  Slider slider;
    private int miningForce;

    
    private void Start()
    {
        sondageView.SetActive(false);
        playerManager = PlayerManager.Instance;
        
    }

    private void addItem()
    {
        Item newItem = ressourceminable[Random.Range(0, ressourceminable.Length)];
        bool isadded = PlayerManager.Instance.AddItem(newItem);
    }
    
    public void onSonderClick()
    {
        sondageView.SetActive(!sondageView.activeSelf);
    }


    public void onStartSondageClick()
    {
        miningForce = ((int) slider.value) * Random.Range(1, 3);
        for (int i = 0; i <= miningForce; i++)
        {
            addItem();
        }
    }
    

}
