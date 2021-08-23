using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.bullet_and_projectile
{
    public class Player : MonoBehaviour, Shootable
    {
        [SerializeField] private CAMP camp;
        [SerializeField] private Object destructionFX;
        [SerializeField, Range(0f, 10f)] private float timeToDie = 1.5f;

        private void Awake()
        {
            PlayerManager.Instance.shipGameObject = gameObject;
        }

        public CAMP getCamp()
        {
            return camp;
        }

        public void takeDamage(int damage)   
        {
            Destruction();
        }


        //'Player's' destruction procedure
        void Destruction()
        {
            Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
            Invoke("gameOver", timeToDie);
            gameObject.SetActive(false);
        
        }

        private void gameOver()
        {
            PlayerManager.Instance.Vie = -1;   
            GameManager.Instance.gameOver();
        }


    }
}