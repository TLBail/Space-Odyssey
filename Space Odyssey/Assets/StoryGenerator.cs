using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoryGenerator : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI text;

    public String[] storys;


    public GameObject TransitionGameObject;
    public GameObject main;

    private void OnEnable()
    {
        title.SetText("Journal de bord n°" + ((int) Random.Range(1, 1000)));
        
        text.SetText(storys[(int) Random.Range(0, storys.Length -1)]);
        
    }
    
    
    
    
    public void  changeOfSystem()
    {
        
        TransitionGameObject.SetActive(false);
        main.SetActive(true);
        GameManager.Instance.SystemView(GameManager.Instance.ActualSystemIndex);

    }
}
