using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    [SerializeField] private Object explosion;

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        
        
        if (!enemyBullet && collision.tag == "Enemy")
        {
            //collision.GetComponent<Enemy>().GetDamage(damage);
            Debug.Log(collision.gameObject.name);
            if (destroyedByCollision)
                Destruction();
        }
    }

    private void OnEnable()
    {
        Invoke("hide", 5f);
    }

    void Destruction() 
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void hide()
    {
        gameObject.SetActive(false);
        
    }
    
}


