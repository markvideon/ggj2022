using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameClock _clock;
    
    private Vector3 _recycledMovementVector = new Vector3();
    [SerializeField] private float playerSpeed = 3f;

    // Bar minimum may not actually be zero for vibes-based reasons
    [SerializeField] private float _barMinimum;
    [SerializeField] private float _barMaximum;
    [SerializeField] private float _currentBar;
    [SerializeField] private float _rechargeRate;
    [SerializeField] private float _consumptionRate;
    

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnUseChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        // Move the bar along
        float rate = 0f;

        if (!_clock.inStasis)
        {
            if (_clock.flow == FlowDirection.forward)
            {
                rate = _rechargeRate;
            } else if (_clock.flow == FlowDirection.backward)
            {
                rate = _consumptionRate;
            }
        }

        float movement = rate * Time.deltaTime;
        
        _currentBar = Mathf.Clamp(_currentBar + movement, _barMaximum, _barMaximum);

        // Determine whether to toggle the flow direction
        float barBefore = _currentBar;
        
        if (_clock.flow == FlowDirection.backward && barBefore + movement < _barMinimum)
        {
            _clock.toggleDirection();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _recycledMovementVector = context.ReadValue<Vector2>();

        float magnitude = Vector3.Magnitude(_recycledMovementVector);
        
        // Sets speed to zero when no input from player
        if (magnitude < FlowDirectionUtility.stasisThreshold)
        {
            _clock.SetSpeed(0f);
        }
        else
        {
            _clock.SetSpeed(magnitude);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var projectileComponent = other.GetComponent<PlayerController>();

        if (projectileComponent)
        {
            Debug.Log("player hit");
        }
    }
    
    private void OnUseChangeDirection()
    {
        _clock.toggleDirection();
    }

    void Start()
    {
        _clock = FindObjectOfType<GameClock>();
        Assert.IsNotNull(_clock);
        Assert.IsTrue(_barMaximum > 0f);
        Assert.IsTrue(_barMaximum > _barMinimum);
    }
    
    public void Update()
    {
        // todo: Handle recharging bar UI here. The state is updated in FixedUpdate.
        
        this.transform.position += _recycledMovementVector * playerSpeed * Time.deltaTime;
    }
}
