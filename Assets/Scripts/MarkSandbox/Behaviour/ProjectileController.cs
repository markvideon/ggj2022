using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
  [SerializeField] private float _moveSpeed = 1f;
  [SerializeField] private GameObject _visualisationPrefab;
  private GameObject _visualisationTower;
  private GameObject _visualisationWallBehindPlayer;
  private Vector3 somewhereFarAway = new Vector3(100f, 100f);
  
  private bool _canMove = false;
  private bool _canCollide = true;
  private GameClock _clock;
  private Transform _tower;
  private Vector3 _inDirectionOfPlayer;
  private FlowDirection _myFlow;

  private Vector3 _workingVector = new Vector3();

  public void Activate(Transform tower, Vector3 inDirectionOfPlayer, GameClock clock, FlowDirection projectileDirection)
  {
    _visualisationTower.transform.position = tower.position;
    _visualisationWallBehindPlayer.transform.position = tower.position + 5f * _inDirectionOfPlayer;
    
    _clock = clock;
    _tower = tower;
    _inDirectionOfPlayer = inDirectionOfPlayer;
    _myFlow = projectileDirection;

    _canMove = true;
  }

  private void FixedUpdate()
  {
    if (_canMove) Move();
  }

  private void Move()
  {
    _workingVector = !FlowDirectionUtility.sameFlowDirection(_myFlow, _clock.flow) ? 
        _inDirectionOfPlayer : 
        -_inDirectionOfPlayer;
    
    this.transform.position += _moveSpeed * Time.deltaTime * _workingVector;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (!_canCollide) return;
    
    var playerComponent = other.collider.GetComponent<PlayerController>();
    
    if (playerComponent)
    {
      PseudoDestroy();
    }
  }

  private void OnHit()
  {
    PseudoDestroy();
  }

  private void PseudoDestroy()
  {
    Debug.Log("Projectile was pseudo-destroyed.");
    _visualisationTower.transform.position = somewhereFarAway;
    _visualisationWallBehindPlayer.transform.position = somewhereFarAway;
    
    _canMove = false;
    _canCollide = false;
    
    // Pool
    var tower = _tower.GetComponent<TowerController>();
    tower.ReturnToPool(this);
  }

  private void Start()
  {
    _visualisationTower = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
    _visualisationWallBehindPlayer = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
  }
}