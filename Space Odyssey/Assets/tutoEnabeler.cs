using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutoEnabeler : MonoBehaviour
{
    public void EnableTuto()
    {
        if(GameManager.Instance.isTutoActiver)
        {
            GameManager.Instance.isTutoActiver = false;
        }
        else
        {
            GameManager.Instance.isTutoActiver = true;
        }
        
    }
}
