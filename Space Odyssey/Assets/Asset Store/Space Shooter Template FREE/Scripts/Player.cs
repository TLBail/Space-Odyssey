﻿using System;
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
    
    private void Awake()
    {
        if (instance == null) 
            instance = this;

        PlayerManager.Instance.shipGameObject = gameObject;
    }

}
















