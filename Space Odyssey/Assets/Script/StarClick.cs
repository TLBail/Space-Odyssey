﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarClick : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private TextMeshPro _text;
    [SerializeField] private GameObject textInfo ; 
    public GameObject transitionObject ; //set lors de l'instansciation dans starPath
    public GameObject main ;//set lors de l'instansciation dans starPath
    private GameObject WarningObj;
    [TextArea] public string pretextCout;
    public int SystemIndex, CoutCarbu;
    public float TimeAnim;
    
    
    private void Start()
    {
         _sprite =  GetComponentInChildren<SpriteRenderer>();
         _text = textInfo.GetComponent<TextMeshPro>();
         _text.text =  pretextCout + CoutCarbu;
         WarningObj = GameManager.Instance.WarningObj;
         
    }

   
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GameManager.Instance.actualSystemIndex == SystemIndex)
        {
            _text.text = "Retour";
            if (Physics.Raycast(ray, out var hit) && hit.transform == transform)
            {
                
                textInfo.SetActive(true);
                _sprite.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.Instance.backToSystemView();
                }
            }
            else
            {
                
                textInfo.SetActive(false);
                _sprite.color = Color.green;
            }
        }else if(GameManager.Instance.actualSystemIndex + 1 == SystemIndex)
        {

            _text.text = pretextCout + CoutCarbu;
            if (Physics.Raycast(ray, out var hit) && hit.transform == transform)
            {
                textInfo.SetActive(true);
                _sprite.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    if (TestCarbu())
                    {
                        if ((int) GameManager.Instance.starPath._numberOfSystem == GameManager.Instance.actualSystemIndex + 1)   // on fait + 1 parce que actual index par encore incrementer on l'incremente juste en desssous
                        {
                            GameManager.Instance.victoire();

                        }
                        else
                        {
                            GOToNextSystem(SystemIndex);
                        }
                    }
                    else
                    {
                        AnimWarning();
                    }

                }
            }
            else
            {
                
                textInfo.SetActive(false);
                _sprite.color = Color.white;
            }
        }else if (GameManager.Instance.actualSystemIndex - 1 == SystemIndex)
        {
            _text.text = pretextCout + CoutCarbu;
            if (Physics.Raycast(ray, out var hit) && hit.transform == transform)
            {
                textInfo.SetActive(true);
                _sprite.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    if (TestCarbu())
                    {
                        GOToNextSystem(SystemIndex);
                    }
                    else
                    {
                        AnimWarning();
                    }
                }
            }
            else
            {
                textInfo.SetActive(false);
                _sprite.color = Color.white;
            }
        }
        else
        {
            textInfo.SetActive(false);
            _sprite.color = Color.red;
        }
        
    }



    private Boolean TestCarbu() => PlayerManager.Instance.Energie > CoutCarbu;


    private void GOToNextSystem(int systemIndex)
    {
        GameManager.Instance.actualSystemIndex = systemIndex;
        PlayerManager.Instance.Energie -= CoutCarbu;
        
        main.SetActive(false);
        transitionObject.SetActive(true);
        Camera.main.gameObject.GetComponent<MovableCamera>()?.resetCamera();
        Camera.main.gameObject.GetComponent<MovableCamera>().movementIsActivated = false;
        
    }
    

    
    
    
    private void AnimWarning()
    {
        WarningObj.SetActive(true); 
        Invoke(nameof(EndAnim), TimeAnim);
    }

    private void EndAnim()
    {
        WarningObj.SetActive(false);
    }
}
