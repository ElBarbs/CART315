using UnityEngine;

public class BreakoutPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float leftWall, rightWall;

    private PlayerControls _controls;
    private Vector2 _movementInput;

    void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new PlayerControls();
            _controls.Enable();

            // Register callbacks for player-specific input.
            if (gameObject.CompareTag("Player"))
            {
                _controls.BreakoutPlayer.MoveRight.started += _ => _movementInput = new Vector2(1, 0);
                _controls.BreakoutPlayer.MoveLeft.started += _ => _movementInput = new Vector2(-1, 0);
                _controls.BreakoutPlayer.MoveRight.canceled += _ => _movementInput = Vector2.zero;
                _controls.BreakoutPlayer.MoveLeft.canceled += _ => _movementInput = Vector2.zero;
            }
        }
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float inputX = _movementInput.x; 
        Vector3 movement = new Vector3(inputX * speed * Time.deltaTime, 0, 0); 
        Vector3 newPosition = transform.position + movement;

        // Clamp the new position within the valid range
        newPosition.x = Mathf.Clamp(newPosition.x, leftWall, rightWall);

        // Apply the clamped position
        transform.position = newPosition;
    }
}
