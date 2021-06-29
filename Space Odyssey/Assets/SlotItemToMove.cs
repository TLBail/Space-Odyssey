using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemToMove : MonoBehaviour
{

    public bool isCLick = false;

    [SerializeField] private Image icon;


    private void Update()
    {
        if (isCLick)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnMove()
    {
        
        if (isCLick)
        {
            icon.enabled = false;
            isCLick = false;
        }
        else
        {
            isCLick = true;
            icon.enabled = true;
            icon.sprite = PlayerManager.Instance.selectedItem.icon;
        }
        
    }
}
