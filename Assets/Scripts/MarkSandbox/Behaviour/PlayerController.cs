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
        float rate = _clock.flow == FlowDirection.forward ? 
            _rechargeRate : -_consumptionRate;

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
        this.transform.position += _recycledMovementVector * playerSpeed * Time.deltaTime;
    }
}
