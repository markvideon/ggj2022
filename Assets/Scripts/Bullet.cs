using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletFrame : Frame
{
    public BulletFrame(Vector3 position, Quaternion rotation, float spawnedTime)
    {
        this.position = position;
        this.rotation = rotation;
        this.spawnedTime = spawnedTime;
    }
    public Vector3 position;
    public Quaternion rotation;
    public float spawnedTime;
}

public class Bullet : BufferedState<Bullet, BulletFrame>
{
    public int damage;
    public float speed;
    public int maxBounces;
    public bool destroyOnHit;
    Rigidbody2D rb;
    GameClock gameClock;
    Vector2 storedVelocity;
    int bounces;
    bool spawnTimeSet;
    SFX sfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<SFX>();
    }

    private void Start()
    {
        base.Start();
        SetOnRead((Frame) =>
        {
            transform.position = Frame.position;
            transform.rotation = Frame.rotation;
            if (gameClock.accumulatedTime <= Frame.spawnedTime)
            {
                sfx.Play(false);
                Destroy(gameObject, sfx.source.clip.length);
            }
        });
        SetOnWrite(() =>
        {
            var newFrame = new BulletFrame(transform.position, transform.rotation, spawnTimeSet ? -100 : gameClock.accumulatedTime);
            spawnTimeSet = true;
            Record(newFrame);
        });
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        storedVelocity = rb.velocity;
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (gameClock.flow == FlowDirection.forward)
        {
            rb.velocity = transform.right * speed * gameClock.flowRate;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health)
        {
            health.TakeDamage(damage);
            if (destroyOnHit && health.takeDamage)
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
