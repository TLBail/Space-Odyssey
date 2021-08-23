using System;
using System.Security.Cryptography;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.bullet_and_projectile
{
    public enum CAMP{
        PLAYER,
        ENNEMI
        
    }
    
    
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] public int damage;
        [SerializeField] public CAMP originprojectileCamp;
        [SerializeField] public Object explosionPrfb;
        [SerializeField, Range(0, 10000)] public float coolDown;
        [SerializeField, Range(0, 1000), Tooltip("100 default value")] public float maxdistanceJoueur = 1000f;

        private float actualCoolDown;


        private void OnEnable()
        {
            actualCoolDown = 0f;
        }

        private void Update()
        {
            actualCoolDown += Time.deltaTime * 1000;
            if (actualCoolDown >= coolDown) destruction();
            if(playerIsToFarAway()) destruction();    
        }

        private bool playerIsToFarAway() =>
            Vector2.Distance(transform.position,
                PlayerManager.Instance.shipGameObject.transform.position)
            >= maxdistanceJoueur;


        private void OnTriggerEnter2D(Collider2D other)
        {
            Shootable objectShooted = other.GetComponent<Shootable>();
            if (objectShooted != null) interactWith(objectShooted);
        }
        
        private void destruction()
        {
            if(explosionPrfb != null) Instantiate(explosionPrfb, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        private void interactWith(Shootable objectShooted)
        {
            if (objectShooted.getCamp() != originprojectileCamp) infligeDamageAndExplode(objectShooted);
        }

        private void infligeDamageAndExplode(Shootable objectShooted)
        {    
            objectShooted.takeDamage(damage);
            destruction();
        }
    }


    
}