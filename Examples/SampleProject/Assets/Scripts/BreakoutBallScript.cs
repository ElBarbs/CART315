using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakoutBallScript : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
        
    public float ballSpeed = 2f;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;
    
    private readonly int[]  _dirOptions = {-1, 1};
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _rigidBody.velocity == Vector2.zero)
        {
            StartCoroutine(nameof(Launch));
        }
    }

    // Start the Ball Moving
    public IEnumerator Launch() {
        yield return new WaitForSeconds(0.5f);
        
        // Add a horizontal force
        _rigidBody.AddForce(transform.right * ballSpeed * _dirOptions[Random.Range(0, _dirOptions.Length)]); // Randomly go Left or Right
        // Add a vertical force
        _rigidBody.AddForce(transform.up * ballSpeed * _dirOptions[Random.Range(0, _dirOptions.Length)]); // Randomly go Up or Down
    }
    
    private void Reset()
    {
        BreakoutGameManager.Instance.lives -= 1;
        _rigidBody.velocity = Vector2.zero;
        ballSpeed = 2f;
        transform.position = new Vector2(0, -2);
    }
    
    // if the ball goes out of bounds
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Wall collision check.
        if (other.gameObject.CompareTag("Wall"))
        {
            SpeedCheck();
        }

        // Player collision check.
        if (other.gameObject.CompareTag("Player"))
        {
            SpeedCheck();
        }

        if (other.gameObject.CompareTag("Brick"))
        {
            SpeedCheck();
            BreakoutGameManager.Instance.points += 20;
            Destroy(other.gameObject, 0.1f);
        }
        
        // Bottom wall collision check.
        if (other.gameObject.CompareTag("Reset"))
        {
            Reset();
        }
    }
    

    private void SpeedCheck() {
        Vector2 velocity = _rigidBody.velocity;

        // Prevent ball from going too fast.
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);

        // Adjust velocity if below minSpeed.
        velocity.x = Mathf.Abs(velocity.x) < minSpeed ? Mathf.Sign(velocity.x) * minSpeed : velocity.x;
        velocity.y = Mathf.Abs(velocity.y) < minSpeed ? Mathf.Sign(velocity.y) * minSpeed : velocity.y;

        _rigidBody.velocity = velocity;
    }
}
