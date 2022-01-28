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

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (active)
        {
            refireRateTimer += Time.deltaTime;
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
        Vector3 dir = player.position - barrelPosition.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        newBullet.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
