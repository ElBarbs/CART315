using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the direction from the enemy to the player.
        Vector2 direction = (GameManager.Instance.GetPlayerPosition() - transform.position).normalized;

        // Move the enemy towards the player.
        _rigidBody2D.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }
}