using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollider;
    public float pullSpeed;


    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        playerCollider.radius = player.CurrentMagnet;


    }
    void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.TryGetComponent(out ICollectible collectable))
            {
                
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 foreceDir = (transform.position - rb.transform.position).normalized;
                rb.AddForce(foreceDir * pullSpeed);

                collectable.Collect();
            }


    }



}
