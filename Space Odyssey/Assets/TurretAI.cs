using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum TurretState
{
    ATTACK,
    IDLE,
    DESTROYED
}

public class TurretAI : MonoBehaviour
{

    public TurretState turretState;
    [Min(0)]
    public int maxDistanceShoot;

    [Range(0, 10000), Tooltip("en millis")]
    public long shootCoolDown;
    public Object bullet;
    public Transform bulletStartingPosition;
    [SerializeField] private int turnSpeed = 10;
    [SerializeField] private int speed = 10;
    [SerializeField] private String PlayerbulletTag;
    [SerializeField]private Object explosion;
    [SerializeField]private int  rotationSpeed;
    public Transform planeteTransform;
    
    private PlayerManager playerManager;
    private Transform playerTransform;

    
    
    
    private float actualCooldown = 0;

    private void Start()
    {
        
        playerManager = PlayerManager.Instance;
        playerTransform = playerManager.shipGameObject.transform;

    }


    private void Update()
    {

        transform.RotateAround(planeteTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        if (turretState == TurretState.IDLE)
        {
            idleMode();
        }
        else if(turretState == TurretState.ATTACK)
        {
            attackMode();

        } else if (turretState == TurretState.DESTROYED)
        {
            //rien faire 
        }
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.CompareTag(PlayerbulletTag))
        {
            blowUp();
        }
        
    }

    private void blowUp()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }

    private void attackMode()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) >= maxDistanceShoot)
        {
            turretState = TurretState.IDLE;
            return;
        }

        rotateTowardThePlayer();
        
        actualCooldown += Time.deltaTime * 1000;
        if(actualCooldown >= shootCoolDown) shootBullet();
        
        
        
        
    }

    private void idleMode()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < maxDistanceShoot)
        {
            turretState = TurretState.ATTACK;
            rotateTowardThePlayer();
            return;
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

    private void shootBullet()
    {
        GameObject bullet = (GameObject) Instantiate(this.bullet, bulletStartingPosition.position, transform.rotation);

        actualCooldown = 0f;
    }
    
}
