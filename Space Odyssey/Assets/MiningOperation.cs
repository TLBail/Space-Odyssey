using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningOperation : MonoBehaviour
{
    public PlayerManager PlayerManager;

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
        miningForce = (int) slider.value;
        
        
        
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
