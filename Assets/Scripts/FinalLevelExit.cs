using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevelExit : MonoBehaviour
{
    [Header("End Game Settings")]
    public string endScreenName = "EndScreen";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            Time.timeScale = 1f;

            Debug.Log("Game Complete! Moving to End Screen.");

            SceneManager.LoadScene(endScreenName);
        }
    }
}