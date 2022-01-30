using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform barrelPosition;
    public float refireRate;
    public bool active;
    public GameObject bullet;
    Transform player;
    float refireRateTimer;
    GameClock gameClock;
    Collider2D collider;

    private void Awake()
    {
        collider = GetComponentInChildren<Collider2D>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        if (!player) return;
        if (gameClock.flow == FlowDirection.backward) return;
        if (active)
        {
            refireRateTimer += Time.deltaTime * gameClock.flowRate;
            if (refireRateTimer >= refireRate)
            {
                Shoot();
                refireRateTimer = 0;
            }
        }
    }

    private void Shoot()
    {
        Transform newBullet = Instantiate(bullet, barrelPosition.position, Quaternion.identity).transform;
        //Physics2D.IgnoreCollision(newBullet.GetComponentInChildren<Collider2D>(), collider);
        Vector3 dir = player.position - barrelPosition.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        newBullet.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
