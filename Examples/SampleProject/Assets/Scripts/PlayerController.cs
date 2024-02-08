using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    private PlayerControls _controls;
    private Vector2 _movementInput;

    void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new PlayerControls();
            _controls.Enable();

            // Register callbacks for player-specific input
            if (gameObject.CompareTag("Player1"))
            {
                _controls.Player1.MoveUp.started += _ => _movementInput = new Vector2(0, 1);
                _controls.Player1.MoveDown.started += _ => _movementInput = new Vector2(0, -1);
                _controls.Player1.MoveUp.canceled += _ => _movementInput = Vector2.zero;
                _controls.Player1.MoveDown.canceled += _ => _movementInput = Vector2.zero;
            }
            else if (gameObject.CompareTag("Player2"))
            {
                _controls.Player2.MoveUp.started += _ => _movementInput = new Vector2(0, 1);
                _controls.Player2.MoveDown.started += _ => _movementInput = new Vector2(0, -1);
                _controls.Player2.MoveUp.canceled += _ => _movementInput = Vector2.zero;
                _controls.Player2.MoveDown.canceled += _ => _movementInput = Vector2.zero;
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
        float inputY = _movementInput.y;
        Vector3 movement = new Vector3(0, inputY * speed * Time.deltaTime, 0);
        Vector3 newPosition = transform.position + movement;

        // Clamp the new position within the valid range
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the clamped position
        transform.position = newPosition;
    }
}
