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
            if (Input.GetKey(KeyCode.Space))
            {
                _speed = _highspeed;
                _particuleEngine.startSize = 2;

            }
            else
            {
                _speed = _normalspeed;
                _particuleEngine.startSize = 1;

            }
            _rigidbody.AddRelativeForce(Vector3.left * (-1 * Input.GetAxis("Horizontal") * Time.deltaTime * _speed / 2));
            _rigidbody.AddRelativeForce(Vector3.up * (Input.GetAxis("Vertical") * Time.deltaTime * _speed));

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _turnSpeed * Time.deltaTime);

        }
    }
}
