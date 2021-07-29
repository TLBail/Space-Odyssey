using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class guidedBulletAi : MonoBehaviour
{
    [Min(0)]
    public int speed;

    public String playerTag = "Player";

    public Object explosion;

    public int maxdistanceJOueur = 1000;

    private PlayerManager playerManager;
    private Transform playerTransform;

    [SerializeField] private int turnSpeed;
    
    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerTransform = playerManager.shipGameObject.transform;
        Invoke("blowUp", 5f);
    }


    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
        rotateTowardThePlayer();
        
        if (Vector2.Distance(transform.position, PlayerManager.Instance.shipGameObject.transform.position) >= maxdistanceJOueur)
        {
            blowUp();
        }

    }

    
    private void  rotateTowardThePlayer()
    {
        
        Vector3 myLocation = transform.position;
        Vector3 targetLocation = playerTransform.position;
        targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position
     
        // vector from this object towards the target location
        Vector3 vectorToTarget = targetLocation - myLocation;
        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
     
        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
     
        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            blowUp();
        }

    }

    private void blowUp()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
}
