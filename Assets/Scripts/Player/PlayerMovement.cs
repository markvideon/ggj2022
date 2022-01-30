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
    private Pause _pausePanel;
    [SerializeField] private Win _winPanel;
    [SerializeField] private Lose _losePanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
        _pausePanel = FindObjectOfType<Pause>();
    }

    public void OnHorizontalInput(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<float>();
    }
    
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _pausePanel.ShowPause();
        }
    }

    public void OnTimeSwitch(InputAction.CallbackContext ctx)
    {
        gameClock.ToggleDirection();
    }
    
    public void OnVerticalInput(InputAction.CallbackContext ctx)
    {
        vertical = ctx.ReadValue<float>();
    }
    
    public void Pause()
    {
        _pausePanel.ShowPause();
    }

    public void OnLose()
    {
        _losePanel.ShowLose();
    }
    
    public void OnWin()
    {
        _winPanel.ShowWin();
    }

    public void RegisterLoseMenu(Lose lose)
    {
        this._losePanel = lose;
    }
    
    public void RegisterPauseMenu(Pause pause)
    {
        this._pausePanel = pause;
    }
    
    public void RegisterWinMenu(Win win)
    {
        this._winPanel = win;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
        //targetSpeed = Mathf.InverseLerp(0, moveSpeed, rb.velocity.magnitude) + minTimeSpeed;
        //gameClock.SetSpeed(Mathf.MoveTowards(gameClock.flowRate, targetSpeed, timeChangeSpeed * Time.deltaTime));
        gameClock.SetSpeed(Mathf.InverseLerp(0, moveSpeed, rb.velocity.magnitude));
        //print(gameClock.flowRate);
    }

}
