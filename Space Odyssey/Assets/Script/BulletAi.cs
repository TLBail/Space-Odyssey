using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BulletAi : MonoBehaviour
{

    [Min(0)]
    public int speed;

    public String playerTag = "Player";

    public Object explosion;

    public int maxdistanceJOueur = 1000;

    private void Start()
    {
        Invoke("blowUp", 5f);
    }


    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
        if (Vector2.Distance(transform.position, PlayerManager.Instance.shipGameObject.transform.position) >= maxdistanceJOueur)
        {
            blowUp();
        }

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
