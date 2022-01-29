using System;
using System.Timers;
using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileController : BufferedState<ProjectileController, ProjectileFrame>
{
  [SerializeField] private GameObject _visualisationPrefab;
  private GameObject _visualisationTower;
  private GameObject _visualisationWallBehindPlayer;
  private Vector3 somewhereFarAway = new Vector3(100f, 100f);
  
  [SerializeField] private float _moveSpeed = 1f;
  private bool _canMove = false;
  private bool _canCollide = true;
  private GameClock _clock;
  private Transform _tower;
  private Vector3 _inDirectionOfPlayer;
  private FlowDirection _myFlow;
  
  private FlowDirection _naturalDirection => _clock.flow;

  private Vector3 _workingVector = new Vector3();
  private float _historySampleMillis = 500f;
  private float _timeSinceLastRecord;
  
  public void Activate(Transform tower, Vector3 inDirectionOfPlayer, GameClock clock, FlowDirection projectileDirection)
  {
    this.transform.name = "Projectile";
    ToggleVisualiserNames();

    _inDirectionOfPlayer = inDirectionOfPlayer;
    _clock = clock;
    
    _tower = tower;
    _myFlow = projectileDirection;
    
    this.transform.position = _tower.transform.position + 0.5f * _inDirectionOfPlayer;
    
    // Needs to start a safe distance away (with current logic) to prevent projectile blowing up tower right away
    _visualisationTower.transform.position = _tower.position;
    _visualisationWallBehindPlayer.transform.position = _tower.position + 15f * _inDirectionOfPlayer;

    _canCollide = true;
    _canMove = true;
  }

  private void FixedUpdate()
  {
    base.FixedUpdate();
    if (_canMove)
    {
      Move();
    }
  }

  private void Move()
  {
    Assert.IsTrue(_canCollide);

    if (!_clock.inStasis)
    {
      if (_clock.flow == FlowDirection.forward)
      {
        // Only record frames if player is moving forward, otherwise the buffer would be
        // cleared and filled with a bunch of empty frames when the player stops moving.
        _workingVector = FlowDirectionUtility.sameFlowDirection(_naturalDirection, _clock.flow) ? 
          _inDirectionOfPlayer : 
          -_inDirectionOfPlayer;
        
        this.transform.position += _moveSpeed * Time.deltaTime * _workingVector;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!_canCollide) return;
    
    var playerComponent = other.GetComponent<PlayerController>();
    var wallComponent = other.GetComponent<WallController>();
    var towerComponent = other.GetComponent<TowerController>();

    if (towerComponent)
    {
      if (FlowDirectionUtility.sameFlowDirection(_naturalDirection, _clock.flow))
      {
        // Projectile is being fired or is heading back into the cannon,
        // In either case, no harm being done
      }
      else
      {
        // Destructive collision. Tower not expecting to fire
        PseudoDestroy();
      }
    }
    else
    {
      if (playerComponent || wallComponent)
      {
        PseudoDestroy();
      }
    }
  }
  
  private void PseudoDestroy()
  {
    Assert.IsNotNull(_clock);
    
    ToggleVisualiserNames();
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
    base.Start();
    _visualisationTower = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
    _visualisationWallBehindPlayer = Instantiate(_visualisationPrefab, somewhereFarAway, Quaternion.identity);
    
    ToggleVisualiserNames();

    SetOnRead((frame) =>
    {
      _canCollide = frame.canCollide;
      _canMove = frame.canMove;
      this.transform.position = frame.currentPosition;
      _inDirectionOfPlayer = frame.inDirectionOfPlayer;
      _moveSpeed = frame.moveSpeed;
      _myFlow = frame.myFlow;
    });
    
    SetOnWrite(() =>
    {
      var frame = new ProjectileFrame(
        canCollide: _canCollide,
        canMove: _canMove,
        currentPosition: this.transform.position,
        inDirectionOfPlayer: new Vector3(_inDirectionOfPlayer.x, _inDirectionOfPlayer.y, _inDirectionOfPlayer.z),
        moveSpeed: _moveSpeed,
        myFlow: _myFlow
      );

      Record(frame);
    });
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