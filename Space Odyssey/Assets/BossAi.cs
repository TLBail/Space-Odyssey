using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public enum BossState
{
    PHASE1,
    PHASE2,
    PHASE3
}

public class BossAi : MonoBehaviour
{
    
    [Header("ship")]
    [SerializeField] private int turnSpeed = 10;
    [SerializeField] private int speed = 10;
    [SerializeField] private Object explosion;
    [SerializeField] private int  shiprotationSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _bosseSprites;
    [SerializeField, Tooltip("chiffre sur lequel l'AI se base pour le phase")] private int maxconstitution;
    [SerializeField] private int playerprojectileDps;

    [Header("mini Torret")]
    [SerializeField, Range(0, 10000), Tooltip("en millis")] private long shootCoolDown;
    [SerializeField] private String PlayerbulletTag;
    [SerializeField] private Object bullet;
    [SerializeField] private Transform[] bulletStartingPositions;
    [SerializeField] private Transform[] torretTransforms;
    [SerializeField] private int  torretrotationSpeed;

    
    [Header("Big Missile")]
    [SerializeField, Range(0, 10000), Tooltip("en millis")] private long missileCoolDown;
    [SerializeField] private Object guidedMissile;
    [SerializeField] private Transform[] missileStratingTransforms;
    [SerializeField] private Animator missileAnitmatior;

    [Header("Wing")] 
    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    
    private int actualconstitution;
    private PlayerManager playerManager;
    private Transform playerTransform;
    private BossState bossState;
    private float shootactualCooldown = 0;
    private float missileactualCooldown = 0;

    private void Start()
    {
        
        playerManager = PlayerManager.Instance;
        playerTransform = playerManager.shipGameObject.transform;
        actualconstitution = maxconstitution;

    }


    private void Update()
    {

        transform.RotateAround(Vector3.zero, Vector3.forward, shiprotationSpeed * Time.deltaTime);
        
        if (bossState == BossState.PHASE1)
        {
            modePhase1();
        }
        else if(bossState == BossState.PHASE2)
        {
            modePhase2();

        } else if (bossState == BossState.PHASE3)
        {
            modePhase3();
        }
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        onBulletEnter(other);
    }

    public void onBulletEnter(Collider2D other)
    {
        if (other.CompareTag(PlayerbulletTag))
        {
            actualconstitution -= playerprojectileDps;
        }

    }
    
    private void blowUp()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
    }

    
    private void modePhase1()
    {
        if (actualconstitution <= (maxconstitution * 0.60) )
        {
            blowUp();
            _spriteRenderer.sprite = _bosseSprites[1];
            rightWing.SetActive(false);
            bossState = BossState.PHASE2;
            return;
        }

        foreach (Transform torretTransform in torretTransforms)
        {
            rotateTowardThePlayer(torretTransform);
        }
        
        shootactualCooldown += Time.deltaTime * 1000;
        if(shootactualCooldown >= shootCoolDown) shootBullet();

        
    }
    

    private void modePhase2()
    {
        if (actualconstitution <= (maxconstitution * 0.30) )
        {
            blowUp();
            _spriteRenderer.sprite = _bosseSprites[2];
            leftWing.SetActive(false);
            bossState = BossState.PHASE3;
            return;
        }

        
        
        missileactualCooldown += Time.deltaTime * 1000;
        if(missileactualCooldown >= missileCoolDown) shootMissile();

        
    }

    
    private void modePhase3()
    {
        if (actualconstitution <= 0)
        {
            blowUp();
            Destroy(this.gameObject);
            return;
        }

        
        foreach (Transform torretTransform in torretTransforms)
        {
            rotateTowardThePlayer(torretTransform);
        }
        
        shootactualCooldown += Time.deltaTime * 1000;
        if(shootactualCooldown >= shootCoolDown) shootBullet();
        
        missileactualCooldown += Time.deltaTime * 1000;
        if(missileactualCooldown >= missileCoolDown) shootMissile();

    }


    
    
    
    private void shootBullet()
    {
        foreach (Transform torrettransform in bulletStartingPositions)
        {
            GameObject bullet = (GameObject) Instantiate(this.bullet, torrettransform.position, torrettransform.rotation);
            
        }

        shootactualCooldown = 0f;
    }

    private void shootMissile()
    {
        foreach (Transform missiletransform in missileStratingTransforms)
        {
            GameObject missile = (GameObject) Instantiate(guidedMissile);
            //missile.transform.SetParent(missiletransform);
            missile.transform.position = new Vector3(missiletransform.position.x, missiletransform.position.y, 10);
            missile.transform.rotation = missiletransform.rotation;

        }
        missileAnitmatior.SetTrigger("shoot");
        missileactualCooldown = 0f;
    }
    
    
    
    private void  rotateTowardThePlayer(Transform transformToRotate)
    {
        
        Vector3 myLocation = transformToRotate.position;
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
        transformToRotate.rotation = Quaternion.RotateTowards(transformToRotate.rotation, targetRotation, torretrotationSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);


    }

    
}
