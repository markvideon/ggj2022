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
    private int poolSize = 3;
    private List<ProjectileController> _activeProjectiles = new List<ProjectileController>();
    private List<ProjectileController> _projectilePool = new List<ProjectileController>();

    private static GameClock _clock;
    
    // Recycled vector
    private Vector3 _recycledTargetVector = new Vector3();

    private bool canShoot = true;
    private float lastShotAt = 0f;

    [SerializeField] private FlowDirection _myDirection = FlowDirection.forward;

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerComponent = other.GetComponent<PlayerController>();
        
        if (playerComponent != null && !_activeTargets.Contains(playerComponent.gameObject))
        {
            _activeTargets.Add(playerComponent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var playerComponent = other.GetComponent<PlayerController>();
        
        if (playerComponent != null && _activeTargets.Contains(playerComponent.gameObject))
        {
            _activeTargets.Remove(playerComponent.gameObject);
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
        Assert.IsTrue(_projectilePool.Count > 0);
        Assert.IsTrue(_activeTargets.Count > 0);

        var projectile = _projectilePool[0];
        TakeFromPool(projectile);

        // todo: Calculate wall behind player.
        _recycledTargetVector.x = _activeTargets[0].transform.position.x - this.transform.position.x;
        _recycledTargetVector.y = _activeTargets[0].transform.position.y - this.transform.position.y;
        _recycledTargetVector.z = _activeTargets[0].transform.position.z - this.transform.position.z;
        _recycledTargetVector.Normalize();

        projectile.Activate(this.transform, _recycledTargetVector, _clock, _myDirection);
        
        if (FlowDirectionUtility.sameFlowDirection(_myDirection, _clock.flow))
        {
           // play any required animations, sounds when a projectile fired *from* tower
        }
    }
    
    void Start()
    {
        if (_clock == null) _clock = FindObjectOfType<GameClock>();
        Assert.IsNotNull(_clock);
    }
    
    private void TakeFromPool(ProjectileController projectileController)
    {
        Assert.IsTrue(_activeProjectiles.Contains(projectileController));
        
        if (_activeProjectiles.Contains(projectileController))
        {
            _activeProjectiles.Remove(projectileController);
            _projectilePool.Add(projectileController);
        }
    }
}
