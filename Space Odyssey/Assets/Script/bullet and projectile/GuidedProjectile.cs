using System;
using System.Transactions;
using UnityEngine;

namespace Script.bullet_and_projectile
{
    public class GuidedProjectile : Projectile
    {
        [Min(0)]
        public int speed;
        [SerializeField] private int turnSpeed;

        
        private PlayerManager playerManager;
        private Transform playerTransform;

        private void Awake()
        {
            playerManager = PlayerManager.Instance;
            playerTransform = playerManager.shipGameObject.transform;
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
            rotateTowardThePlayer();
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


    }
}