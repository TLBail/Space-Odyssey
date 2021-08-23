using System;
using System.Collections;
using System.Collections.Generic;
using Script.Item_and_inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SondageOperation : MonoBehaviour
{

    [SerializeField]
    private GameObject sondageView;

    [SerializeField]
    private Item[] ressourceminable;

    public  Slider slider;
    private int miningForce;

    private Inventaire inventaire;
    
    private void Start()
    {
        sondageView.SetActive(false);
        inventaire = GameManager.Instance.inventaire;
    }

    private void addItem()
    {
        Item newItem = ressourceminable[Random.Range(0, ressourceminable.Length)];
        bool isadded = inventaire.addItem(newItem);
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
