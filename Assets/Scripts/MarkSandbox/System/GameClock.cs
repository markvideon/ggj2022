using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameClock : MonoBehaviour
{
    // Getter is utilised so that we can use the settable value with SerializeField
    [SerializeField] private FlowDirection _flow = FlowDirection.forward;
    public FlowDirection flow { get { return _flow; } }
    public bool inStasis { get { return _flowRate < FlowDirectionUtility.stasisThreshold; } }
    
    private SandboxFlowVisualiser _visualiser;
    public float flowRate { get { return _flowRate; } }
    private float _flowRate = 1f;

    // A stand-in for [Time.time] for any entity affected by the [GameClock]. 
    public float accumulatedTime { get { return _accumulatedTime; }
    }
    private float _accumulatedTime;

    public void FixedUpdate()
    {
        if (!inStasis) _accumulatedTime += Time.deltaTime;
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
        _visualiser = FindObjectOfType<SandboxFlowVisualiser>();
        Assert.IsNotNull(_visualiser);
        SetSpeed(0f);
    }

    public void toggleDirection()
    {
        Assert.IsFalse(_flow == FlowDirection.error);

        if (_flow == FlowDirection.forward)
        {
            _flow = FlowDirection.backward;
        }
        else if (_flow == FlowDirection.backward)
        {
            _flow = FlowDirection.forward;
        }

        _visualiser.SetText("Global direction: " + FlowDirectionUtility.directionAsString(_flow));
    }
}
