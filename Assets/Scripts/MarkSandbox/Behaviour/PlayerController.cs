using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameClock _clock;
    
    private Vector3 _recycledMovementVector = new Vector3();
    [SerializeField] private float playerSpeed = 3f;

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnUseChangeDirection();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _recycledMovementVector = context.ReadValue<Vector2>();
        Debug.Log(_recycledMovementVector);
    }
    
    private void OnUseChangeDirection()
    {
        _clock.toggleDirection();
    }

    void Start()
    {
        _clock = FindObjectOfType<GameClock>();
        Assert.IsNotNull(_clock);
    }
    
    public void Update()
    {
        this.transform.position += _recycledMovementVector * playerSpeed * Time.deltaTime;
    }
}
