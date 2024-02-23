using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Check if hit by the correct bullet color.
            var enemyColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            var bulletColor = _spriteRenderer.color;
            
            if ((int)enemyColor.r == (int)bulletColor.r && (int)enemyColor.g == (int)bulletColor.g && (int)enemyColor.b == (int)bulletColor.b)
            {
                Destroy(other.gameObject);
                GameManager.Instance.IncreaseScore(50);
            }
            
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}