using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public bool destroyOnHit;
    Rigidbody2D rb;
    GameClock gameClock;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed * gameClock.flowRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health)
        {
            health.TakeDamage(damage);
            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
            return;
        }
        else
        {
            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
