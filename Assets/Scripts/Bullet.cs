using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletFrame : Frame
{
    public BulletFrame(Vector3 position, Quaternion rotation, float spawnedTime, bool playHitThisFrame)
    {
        this.position = position;
        this.rotation = rotation;
        this.spawnedTime = spawnedTime;
        this.playHitThisFrame = playHitThisFrame;
    }
    public Vector3 position;
    public Quaternion rotation;
    public float spawnedTime;
    public bool playHitThisFrame;
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
    public SFX spawnSFX, bounceSFX;
    bool bufferHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnSFX = GetComponent<SFX>();
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
                spawnSFX.Play(false);
                Destroy(gameObject, spawnSFX.source.clip.length);
            }
            if (Frame.playHitThisFrame) bounceSFX.Play(false);
        });
        SetOnWrite(() =>
        {
            var newFrame = new BulletFrame(transform.position, transform.rotation, spawnTimeSet ? -100 : gameClock.accumulatedTime, bufferHit);
            spawnTimeSet = true;
            bufferHit = false;
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
            bufferHit = true;
            bounceSFX.Play(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
