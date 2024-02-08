using System.Collections;
using TMPro;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
        
    public float ballSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;

    public AudioClip ballSound, scoreSound;
    
    // Score variables.
    public int leftPlayerScore,  rightPlayerScore;
    public TextMeshPro leftScoreText, rightScoreText;
    
    private readonly int[]  _dirOptions = {-1, 1};
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.clip = ballSound;
        Reset();
    }
    
    // Start the Ball Moving
    public IEnumerator Launch() {
        yield return new WaitForSeconds(1.5f);
        
        // Add a horizontal force
        _rigidBody.AddForce(transform.right * ballSpeed * _dirOptions[Random.Range(0, _dirOptions.Length)]); // Randomly go Left or Right
        // Add a vertical force
        _rigidBody.AddForce(transform.up * ballSpeed * _dirOptions[Random.Range(0, _dirOptions.Length)]); // Randomly go Up or Down
    }
    
    private void Reset() {
        _rigidBody.velocity = Vector2.zero;
        ballSpeed = 2;
        transform.position = new Vector2(0, -2);
        StartCoroutine(nameof(Launch));
    }
    
    // if the ball goes out of bounds
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Wall collision check.
        if (other.gameObject.CompareTag("Wall"))
        {
            _audio.clip = ballSound;
            _audio.pitch = 0.75f;
            _audio.Play();
            SpeedCheck();
        }

        // Player collision check.
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            _audio.clip = ballSound;
            _audio.pitch = 1.0f;
            _audio.Play();
            SpeedCheck();
        }
        
        // did we hit the left Wall?
        if (other.gameObject.CompareTag("LeftWall"))
        {
            _audio.clip = scoreSound;
            _audio.Play();
            rightPlayerScore += 1;
            UpdateScoreText();
            Reset();
        }
        
        // did we hit the right Wall?
        if (other.gameObject.CompareTag("RightWall"))
        {
            _audio.clip = scoreSound;
            _audio.Play();
            leftPlayerScore += 1;
            UpdateScoreText();
            Reset();
        }
    }
    
    private void UpdateScoreText()
    {
        // Update UI Text elements with the current scores
        if (leftScoreText != null)
            leftScoreText.text = leftPlayerScore.ToString();

        if (rightScoreText != null)
            rightScoreText.text = rightPlayerScore.ToString();
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
