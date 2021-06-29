using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed, minCam, maxCam;
    [SerializeField] private Transform _player;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        //transform.Translate(Vector3.forward * Input.mouseScrollDelta * Time.deltaTime * _scrollSpeed);
        Camera.main.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * _scrollSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCam, maxCam);

        
    }
}
