using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 

public class AttackHitbox : MonoBehaviour
{
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

    public void CheckForKills()
    {
        if (SceneManager.GetActiveScene().name == "World4")
        {
            Debug.Log("Attacks are useless in World4! Enemies are immortal.");
            enemiesInRange.Clear(); 
            return; 
        }

        Debug.Log("Checking for kills... Items in list: " + enemiesInRange.Count); 
        
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            GameObject enemy = enemiesInRange[i];

            if (enemy != null)
            {
                Debug.Log("Confirmed Kill after 1 second!");
                Destroy(enemy);
            }
        }
        
        enemiesInRange.Clear();
    }
}