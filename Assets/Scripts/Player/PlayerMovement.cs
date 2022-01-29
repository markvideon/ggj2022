using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, minTimeSpeed, timeChangeSpeed;
    Rigidbody2D rb;
    float horizontal, vertical;
    GameClock gameClock;
    float targetSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    public void OnHorizontalInput(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<float>();
    }

    public void OnVerticalInput(InputAction.CallbackContext ctx)
    {
        vertical = ctx.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
        targetSpeed = Mathf.InverseLerp(0, moveSpeed, rb.velocity.magnitude) + minTimeSpeed;
        gameClock.SetSpeed(Mathf.MoveTowards(gameClock.flowRate, targetSpeed, timeChangeSpeed * Time.deltaTime));
        //print(gameClock.flowRate);
    }

}
