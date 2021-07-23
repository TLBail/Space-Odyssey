using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;
    public String ennemiBulletTag;
    [SerializeField] private float timeToDie = 1.5f;
    
    private void Awake()
    {
        if (instance == null) 
            instance = this;

        PlayerManager.Instance.shipGameObject = gameObject;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        Destruction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.CompareTag(ennemiBulletTag)) Destruction();
    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Invoke("gameOver", timeToDie);
        gameObject.SetActive(false);
        
    }

    private void gameOver()
    {
        PlayerManager.Instance.Vie = -1;   
        GameManager.Instance.GameOver();
    }
}
















