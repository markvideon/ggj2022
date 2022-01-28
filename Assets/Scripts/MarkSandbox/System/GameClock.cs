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

    private SandboxFlowVisualiser _visualiser;
    private void Start()
    {
        _visualiser = FindObjectOfType<SandboxFlowVisualiser>();
        Assert.IsNotNull(_visualiser);
    }

    public void toggleDirection()
    {
        Debug.Log("Entering toggleDirection. Current value of flow: " + _flow.ToString());
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
        Debug.Log("Exiting toggleDirection. Current value of flow: " + _flow.ToString());

    }
}
