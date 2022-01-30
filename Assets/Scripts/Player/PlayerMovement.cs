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

    [SerializeField] private Menu _activeMenu;
    private Pause _pausePanel;
    private Win _winPanel;
    private Lose _losePanel;

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
            _activeMenu = _pausePanel;
            _pausePanel.ShowMenu();
        }
    }

    public void OnCancelUI(InputAction.CallbackContext ctx)
    {
        _activeMenu.HideMenu();
        _activeMenu = null;
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
        _pausePanel.ShowMenu();
    }

    public void OnLose()
    {
        _activeMenu = _losePanel;
        _losePanel.ShowMenu();
    }
    
    public void OnWin()
    {
        _activeMenu = _winPanel;
        _winPanel.ShowMenu();
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
