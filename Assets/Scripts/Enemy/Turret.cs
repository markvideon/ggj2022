using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform barrelPosition;
    public float refireRate;
    public bool active, fireInstantly;
    public GameObject bullet;
    Transform player;
    float refireRateTimer;
    GameClock gameClock;
    Collider2D collider;
    TowerAnimation anim;

    private void Awake()
    {
        collider = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<TowerAnimation>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        gameClock = FindObjectOfType<GameClock>();
        if (fireInstantly) PrepareShot(true);
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
                PrepareShot(false);
                active = false;
            }
        }
    }

    void PrepareShot(bool fireInstantly)
    {
        //The animation controller will run Shoot() below when it gets to the right sprite. kinda wonky but it is what it is
        anim.ShootAnimation(fireInstantly);
    }

    //Called by TowerAnimation.cs
    public void Shoot()
    {
        Transform newBullet = Instantiate(bullet, barrelPosition.position, Quaternion.identity).transform;
        //Physics2D.IgnoreCollision(newBullet.GetComponentInChildren<Collider2D>(), collider);
        Vector3 dir = player.position - barrelPosition.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        newBullet.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        refireRateTimer = 0;
        active = true;
    }
}
