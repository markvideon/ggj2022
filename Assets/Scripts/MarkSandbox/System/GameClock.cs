using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameClock : BufferedState<GameClock, GameClockFrame>
{
    // Getter is utilised so that we can use the settable value with SerializeField
    [SerializeField] private FlowDirection _flow = FlowDirection.forward;
    public FlowDirection flow { get { return _flow; } }
    public bool inStasis { get { return _flowRate < FlowDirectionUtility.stasisThreshold; } }
    
    private SandboxFlowVisualiser _visualiser;
    private GameClockHistoryVisualiser _historyVisualiser;
    
    private float _flowRate = 1f;
    public float flowRate { get { return _flowRate;  } }

    // A stand-in for [Time.time] for any entity affected by the [GameClock]. 
    public float accumulatedTime { get { return _accumulatedTime; } }
    public float deltaTime { get { return _deltaTime; } }
    
    private float _accumulatedTime;
    private float _deltaTime;

    public float timeSinceChange { get { return _timeSinceChange; } }
    private float _timeSinceChange = 0f;

    public void FixedUpdate()
    {
        base.FixedUpdate();
        _historyVisualiser.SetText("Bar remaining: " + percentageBufferUtilised);

        if (!inStasis)
        {
            if (flow == FlowDirection.forward)
            {
                _accumulatedTime += Time.deltaTime;
                _deltaTime = Time.deltaTime;
            }
    
            if (flow == FlowDirection.backward)
            {
                _accumulatedTime -= Time.deltaTime;
                _deltaTime = -Time.deltaTime;
            }

            _timeSinceChange += Time.deltaTime;
        }
    }

    public void SetDirection(FlowDirection next)
    {
        _flow = next;
        _timeSinceChange = 0f;
        _historyVisualiser.SetText("Bar remaining: " + percentageBufferUtilised);
        if (!inStasis) _visualiser.SetText("Global direction: " + FlowDirectionUtility.directionAsString(_flow));
    }

    public void SetSpeed(float input)
    {
        _flowRate = input;
        
        if (inStasis)
        {
            _visualiser.SetText("Global direction: Stasis");
        }
        else
        {
            _visualiser.SetText("Global direction: " + FlowDirectionUtility.directionAsString(_flow));
        }
    }
    
    private void Start()
    {
        base.Start();
        _visualiser = FindObjectOfType<SandboxFlowVisualiser>();
        _historyVisualiser = FindObjectOfType<GameClockHistoryVisualiser>();
        Assert.IsNotNull(_visualiser);
        Assert.IsNotNull(_historyVisualiser);
        
        // Initially, GameClock is zero as the player does not move
        SetSpeed(0f);
        
        // Callbacks that happens 'under the hood' in BufferedState
        SetOnRead((blob) =>
        {
            _accumulatedTime = blob._accumulatedTime;
            // Note the negative
            _deltaTime = -blob._deltaTime;
            _flowRate = blob._flowRate;
        });
        
        SetOnWrite(() =>
        {
            var frame = new GameClockFrame(
                _accumulatedTime,
                _deltaTime,
                _flowRate
            );

            Record(frame);
        });
    }

    public void ToggleDirection()
    {
        Assert.IsFalse(_flow == FlowDirection.error);

        if (_flow == FlowDirection.forward)
        { 
            SetDirection(FlowDirection.backward);
            _flow = FlowDirection.backward;
        }
        else if (_flow == FlowDirection.backward)
        {
            SetDirection(_flow = FlowDirection.forward);
        }
    }
}
