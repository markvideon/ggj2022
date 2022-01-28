using System;
using UnityEngine;
using UnityEngine.Assertions;

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
    this.transform.name = "Projectile";
    ToggleVisualiserNames();

    _inDirectionOfPlayer = inDirectionOfPlayer;
    _clock = clock;
    _tower = tower;
    _myFlow = projectileDirection;
    
    this.transform.position = _tower.transform.position + 1f * _inDirectionOfPlayer;
    
    // Needs to start a safe distance away (with current logic) to prevent projectile blowing up tower right away
    _visualisationTower.transform.position = _tower.position;
    _visualisationWallBehindPlayer.transform.position = _tower.position + 15f * _inDirectionOfPlayer;

    _canCollide = true;
    _canMove = true;
  }

  private void FixedUpdate()
  {
    if (_canMove) Move();
  }

  private void Move()
  {
    Assert.IsTrue(_canCollide);

    if (!_clock.inStasis)
    {
      _workingVector = FlowDirectionUtility.sameFlowDirection(_myFlow, _clock.flow) ? 
        _inDirectionOfPlayer : 
        -_inDirectionOfPlayer;
      this.transform.position += _moveSpeed * Time.deltaTime * _workingVector;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!_canCollide) return;
    
    var playerComponent = other.GetComponent<PlayerController>();
    var wallComponent = other.GetComponent<WallController>();
    var towerComponent = other.GetComponent<TowerController>();
    
    if (playerComponent || wallComponent || towerComponent)
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
    ToggleVisualiserNames();
    Debug.Log("Projectile was pseudo-destroyed.");
    _visualisationTower.transform.position = somewhereFarAway;
    _visualisationWallBehindPlayer.transform.position = somewhereFarAway;
    
    _canMove = false;
    _canCollide = false;
    
    this.transform.name = "(Pooled) Projectile";
    this.transform.position = somewhereFarAway;

    var tower = _tower.GetComponent<TowerController>();
    tower.ReturnToPool(this);
  }

  private void Start()
  {
    _visualisationTower = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
    _visualisationWallBehindPlayer = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
    ToggleVisualiserNames();
  }

  private void ToggleVisualiserNames()
  {
    if (_canMove)
    {
      _visualisationTower.name = "ProjectileVisualiser Point A";
      _visualisationWallBehindPlayer.name = "ProjectileVisualiser Point B";
    }
    else
    {
      _visualisationTower.name = "(Not in use) ProjectileVisualiser Point A";
      _visualisationWallBehindPlayer.name = "(Not in use) ProjectileVisualiser Point B";
    }

  }
}