using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing levels

public class SceneChanger : MonoBehaviour
{
    [Header("Settings")]
    public string nextSceneName; // Type the name of your next scene here

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object touching the portal is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete! Loading: " + nextSceneName);
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        // Option A: Load by the name you typed in the Inspector
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Option B: Just load the very next scene in the build list
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}