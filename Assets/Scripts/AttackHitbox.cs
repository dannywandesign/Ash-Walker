using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    // A list to track enemies currently inside the "swing" zone
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    // This is called by the PlayerMovement script after the 1s delay
    public void CheckForKills()
    {
        Debug.Log("Checking for kills... Items in list: " + enemiesInRange.Count); // ADD THIS
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            GameObject enemy = enemiesInRange[i];

            if (enemy != null)
            {
                Debug.Log("Confirmed Kill after 1 second!");
                Destroy(enemy);
            }
        }
        
        // Clear the list completely for the next swing
        enemiesInRange.Clear();
    }
}