using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingScript : MonoBehaviour
{

    [SerializeField] private BossAi _bossAi;


    private void OnTriggerEnter2D(Collider2D other)
    {
        _bossAi.onBulletEnter(other);
    }
}
