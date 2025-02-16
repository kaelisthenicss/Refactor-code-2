using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("EnemyBullet"))
        {
            // Enemy bullets should only return to the pool if they hit the Player or the Wall
            if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
            {
                ObjectPoolManager.Instance.ReturnEnemyBullet(gameObject);
            }
        }
        else // This means it's a Player bullet
        {
            // Player bullets return to the pool when hitting Walls, Enemies, or other bullets
            if (collision.CompareTag("Wall") || collision.CompareTag("Enemy") || collision.CompareTag("Bullet"))
            {
                ObjectPoolManager.Instance.ReturnPlayerBullet(gameObject);
            }
        }
    }

}