using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Assertions;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    // Targets e.g. Player
    private List<GameObject> _activeTargets = new List<GameObject>();
    
    // Projectiles
    private int poolSize = 2;
    private List<ProjectileController> _activeProjectiles = new List<ProjectileController>();
    private List<ProjectileController> _projectilePool = new List<ProjectileController>();

    private static GameClock _clock;
    
    // Recycled vector
    private Vector3 _recycledTargetVector = new Vector3();
    private Vector3 somewhereFarAway = new Vector3(100f, 100f);

    private bool canShoot = false;
    private float lastShotAt = 0f;
    private float minimumTimeBetweenShotsMillis = 2000f;

    [SerializeField] private FlowDirection _myDirection = FlowDirection.forward;

    private void FixedUpdate()
    {
        var millisSinceLastShot = 1000f * (_clock.accumulatedTime - lastShotAt);
        
        if (!canShoot && _projectilePool.Count > 0 && millisSinceLastShot >= minimumTimeBetweenShotsMillis)
        {
            canShoot = true;
        }

        if (canShoot && _activeTargets.Count > 0 && _clock.flow == FlowDirection.forward)
        {
            Shoot();
        }
    }
    
    public void RemoveTargetIfLost(GameObject possibleTarget)
    {
        if (_activeTargets.Contains(possibleTarget))
        {
            _activeTargets.Remove(possibleTarget);
        }
    }

    public void ReturnToPool(ProjectileController projectileController)
    {
        Assert.IsTrue(_activeProjectiles.Contains(projectileController));
        
        if (_activeProjectiles.Contains(projectileController))
        {
            _activeProjectiles.Remove(projectileController);
            _projectilePool.Add(projectileController);
        }
    }

    private void Shoot()
    {
        canShoot = false;
        Assert.IsTrue(_projectilePool.Count > 0);
        Assert.IsTrue(_activeTargets.Count > 0);

        var projectile = _projectilePool[0];
        TakeFromPool(projectile);

        _recycledTargetVector.x = _activeTargets[0].transform.position.x - this.transform.position.x;
        _recycledTargetVector.y = _activeTargets[0].transform.position.y - this.transform.position.y;
        _recycledTargetVector.z = _activeTargets[0].transform.position.z - this.transform.position.z;
        _recycledTargetVector.Normalize();
        
        projectile.Activate(this.transform, _recycledTargetVector, _clock, _myDirection);
        
        if (FlowDirectionUtility.sameFlowDirection(_myDirection, _clock.flow))
        {
           // play any required animations, sounds when a projectile fired *from* tower
        }

        lastShotAt = _clock.accumulatedTime;
    }
    
    public void SpotTargetIfNew(GameObject possibleNewTarget)
    {
        if (!_activeTargets.Contains(possibleNewTarget))
        {
            _activeTargets.Add(possibleNewTarget);
        }
    }
    
    void Start()
    {
        if (_clock == null) _clock = FindObjectOfType<GameClock>();
        Assert.IsNotNull(_clock);
        
        for (int i = 0; i < poolSize; i++)
        {
            var prefabInstance = Instantiate(projectilePrefab, somewhereFarAway, Quaternion.identity);
            var projectileController = prefabInstance.GetComponent<ProjectileController>();
            Assert.IsNotNull(projectileController);
            _projectilePool.Add(projectileController);
        }
    }
    
    private void TakeFromPool(ProjectileController projectileController)
    {
        Assert.IsTrue(_projectilePool.Contains(projectileController));
        
        if (_projectilePool.Contains(projectileController))
        {
            _projectilePool.Remove(projectileController);
            _activeProjectiles.Add(projectileController);
        }
    }
}
