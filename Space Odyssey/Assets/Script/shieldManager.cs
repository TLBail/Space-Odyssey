using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldManager : MonoBehaviour
{

    [SerializeField] private float maxVie;
    [SerializeField] private String tagBullet;
    [SerializeField] private int damageForEveryBullet;
    [SerializeField, Range(0, 50)] private float desactivationTime;

    private CapsuleCollider2D _capsuleCollider2D;
    private SpriteRenderer _spriteRenderer;
    private float vie;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        restart();
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagBullet))
        {
            vie -= damageForEveryBullet;
            if (vie <= 0)
            {
                _capsuleCollider2D.enabled = false;
                Invoke("restart", desactivationTime);        
            }
            
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, vie / maxVie / 2);
        }
    }

    public void restart()
    {
        _capsuleCollider2D.enabled = true;
        vie = maxVie;
    }


}
