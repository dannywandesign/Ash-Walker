using UnityEngine;
using UnityEngine.SceneManagement; // Useful if you want to restart the whole level

public class Deathzone : MonoBehaviour
{
    // You can set these in the Inspector to Peter's starting coordinates
    public Vector2 spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the zone is the Player
        if (other.CompareTag("Player"))
        {
            // Option A: Teleport the player back to the start
            other.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, other.transform.position.z);
            
            // Optional: Reset the player's velocity so they don't keep falling/moving
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            Debug.Log("Peter fell! Returning to start.");
        }
    }
}