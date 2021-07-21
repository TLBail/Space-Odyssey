using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiningOperation : MonoBehaviour
{
    public PlayerManager playerManager;

    public Slider slider;

    private int miningForce;

    public  Animator animator;

    public float timeTransition = 0.5f;
    [SerializeField]
    public Item[] ressourceminable;

    
    private void addItem()
    {
        Item newItem = ressourceminable[Random.Range(0, ressourceminable.Length)];
        bool isadded = PlayerManager.Instance.AddItem(newItem);
    }
    
    public void mineDesRessource()
    {
        gameObject.SetActive(true);
        
        
        
        
    }


    public void onLanceOperation()
    {
        miningForce = ((int) slider.value) * Random.Range(1, 3);
        for (int i = 0; i <= miningForce; i++)
        {
            addItem();
        }
    }


    public void onClosePanel()
    {
        animator.SetTrigger("closePanel");
        Invoke("HideGameObject", timeTransition);
    }


    private void HideGameObject()
    {
        gameObject.SetActive(false);
    }
    
}
