using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true;
    [SerializeField] private float _normalspeed, _turnSpeed, _highspeed;
    private float _speed;
    private Rigidbody2D _rigidbody;
    [SerializeField] private ParticleSystem _particuleEngine;

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling 

            if (Input.GetKey(KeyCode.Space))
            {
                _speed = _highspeed;
                


#pragma warning disable CS0618 // Le type ou le membre est obsolète
                _particuleEngine.startSize = 2;
#pragma warning restore CS0618 // Le type ou le membre est obsolète

            }
            else
            {
                _speed = _normalspeed;


#pragma warning disable CS0618 // Le type ou le membre est obsolète
                _particuleEngine.startSize = 1;
#pragma warning restore CS0618 // Le type ou le membre est obsolète

            }
            transform.Rotate(Vector3.back * Input.GetAxis("Horizontal") * Time.deltaTime * _turnSpeed);
            _rigidbody.AddRelativeForce(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * _speed);


               
#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            if (Input.touchCount == 1) // if there is a touch
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
#endif
           
        }
    }


    
}
