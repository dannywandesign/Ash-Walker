using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ShardManager.instance != null)
            {
                ShardManager.instance.ResetShards();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            Debug.Log("Peter fell! Shards lost, returning to start.");
        }
    }
}