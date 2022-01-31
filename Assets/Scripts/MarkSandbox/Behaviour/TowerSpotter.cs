using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TowerSpotter : MonoBehaviour
{
    private TowerController _parent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerComponent = other.GetComponent<PlayerController>();
        
        if (playerComponent != null)
        {
            _parent.SpotTargetIfNew(playerComponent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var playerComponent = other.GetComponent<PlayerController>();
        
        if (playerComponent != null)
        {
            _parent.RemoveTargetIfLost(playerComponent.gameObject);
        }
    }

    private void Start()
    {
        _parent = this.transform.parent.GetComponent<TowerController>();
        Assert.IsNotNull(_parent);
    }
}
