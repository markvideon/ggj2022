using UnityEngine;

// Frames are subsets of the state of MonoBehaviours that 
// are to be buffered. Create as required.
public class Frame { }

public class GameClockFrame : Frame
{
  public GameClockFrame(
    float accumulatedTime,
    float deltaTime,
    float flowRate
  )
  {
    _accumulatedTime = accumulatedTime;
    _deltaTime = deltaTime;
    _flowRate = flowRate;
  }

  public float _accumulatedTime;
  public float _deltaTime;
  public float _flowRate;
}

public class ProjectileFrame : Frame
{
  public ProjectileFrame(
    bool canMove, 
    bool canCollide,
    Vector3 currentPosition,
    Vector3 inDirectionOfPlayer,
    float moveSpeed,
    FlowDirection myFlow
  )
  {
    this.canCollide = canCollide;
    this.canMove = canMove;
    this.currentPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
    this.moveSpeed = moveSpeed;
    this.myFlow = myFlow;
    this.inDirectionOfPlayer = inDirectionOfPlayer;
  }

  public bool canMove;
  public bool canCollide;
  public Vector3 currentPosition;
  public Vector3 inDirectionOfPlayer;
  public float moveSpeed;
  public FlowDirection myFlow;
}