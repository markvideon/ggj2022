using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public int maxBounces;
    public bool destroyOnHit;
    Rigidbody2D rb;
    GameClock gameClock;
    Vector2 storedVelocity;
    int bounces;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        storedVelocity = rb.velocity;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed * gameClock.flowRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounces < maxBounces || maxBounces < 0)
        {
            bounces++;
            float speed = storedVelocity.magnitude;
            Vector2 dir = Vector2.Reflect(storedVelocity.normalized, collision.contacts[0].normal).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
