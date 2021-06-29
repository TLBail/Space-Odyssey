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
    
    public void mineDesRessource()
    {
        gameObject.SetActive(true);
        
        
        
        
    }


    public void onLanceOperation()
    {
        miningForce = ((int) slider.value) * Random.Range(1, 3);
        for (int i = 0; i <= miningForce; i++)
        {
            playerManager.MineRessource();
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
