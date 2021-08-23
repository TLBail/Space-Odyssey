using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitMover : MonoBehaviour
{


    [SerializeField] private float rotatinSpeed;
    
    private Transform planetePosition;


    public void setPlanetePosition(Transform planetePosition)
    {
        this.planetePosition = planetePosition;
    }
    
    private void Update()
    {
        transform.RotateAround(planetePosition.position, Vector3.forward, rotatinSpeed * Time.deltaTime);
    }
}
