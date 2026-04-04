using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Vector2 spawnPoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Peter died! Respawning...");
        transform.position = spawnPoint;
        rb.linearVelocity = Vector2.zero; 
    }
}