using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public GameObject colorObject;
    
    private Rigidbody2D _rigidBody;
    private Color _bulletColor;
    private int _health = 100;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _bulletColor = new Color(0f, 0f, 1f);
        colorObject.GetComponent<SpriteRenderer>().color = _bulletColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ColorSwap"))
        {
            var newColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            _bulletColor = newColor;
            colorObject.GetComponent<SpriteRenderer>().color = newColor;
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _health -= 1;
            GameManager.Instance.UpdateHealthUI(_health);
            
            if (_health == 0)
            {
                GameManager.Instance.EndGame();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _rigidBody.velocity = ctx.ReadValue<Vector2>() * speed;
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        var playerPosition = transform.position;
        var moveDirection = (Vector3.zero - playerPosition).normalized;
        
        var newBullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveDirection.x, moveDirection.y) * speed, ForceMode2D.Impulse);
        newBullet.GetComponent<SpriteRenderer>().color = _bulletColor;
    }
}
