using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class MovableCamera : MonoBehaviour
{
    [SerializeField] public float speedX, speedY, minCam, maxCam, scrollSpeed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (speedX * Time.deltaTime * Input.GetAxis("Horizontal")));
        transform.Translate(Vector3.up * (speedY * Time.deltaTime * Input.GetAxis("Vertical")));
        Camera.main.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed * -1;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCam, maxCam);
    }
}
