using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    float horizontal, vertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

}
