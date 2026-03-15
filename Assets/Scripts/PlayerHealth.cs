using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Vector2 spawnPoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set spawn point to wherever Peter starts
        spawnPoint = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If Peter's body touches the Slime's body
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Peter died! Respawning...");
        transform.position = spawnPoint;
        rb.linearVelocity = Vector2.zero; // Stop him from falling/moving instantly
    }
}