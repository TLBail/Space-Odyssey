using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.bullet_and_projectile
{
    public class BasicProjectile : Projectile
    {
        
        [Min(0)]
        public int speed;
        

        private void Update()
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }

    }
}