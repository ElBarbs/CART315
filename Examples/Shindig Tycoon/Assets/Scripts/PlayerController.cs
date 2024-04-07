using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character movement
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 movement; // The current movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the GameObject
    }

    void Update()
    {
        // Input.GetAxisRaw gives us a value of -1, 0, or 1
        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input (left/right)
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical input (up/down)
    }

    void FixedUpdate()
    {
        // Move the character by finding the target velocity
        Vector2 currentPosition = rb.position; // Current position
        Vector2 newPosition = currentPosition + movement * moveSpeed * Time.fixedDeltaTime; // Calculate new position
        rb.MovePosition(newPosition); // Move the Rigidbody to the new position
    }
}
