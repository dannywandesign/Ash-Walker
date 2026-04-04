using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; 

public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    public int worldIndexToUnlock = 1; 
    public string menuSceneName = "Worlds";

    [Header("Notification Settings")]
    public TextMeshProUGUI notificationText;
    public Color lockedTextColor = Color.yellow;
    public float displayDuration = 3f;

    private bool isDisplaying = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ShardManager.instance != null)
            {
                int current = ShardManager.instance.shardCount;
                int total = ShardManager.instance.totalShardsInLevel;

                if (current < total)
                {
                    if (!isDisplaying)
                    {
                        StartCoroutine(ShowLockedMessage(total - current));
                    }
                    return; 
                }
            }

            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None; 

            GameSession.UnlockWorld(worldIndexToUnlock);
            SceneManager.LoadScene(menuSceneName);
        }
    }

    IEnumerator ShowLockedMessage(int shardsLeft)
    {
        isDisplaying = true;

        if (notificationText != null)
        {
            notificationText.color = lockedTextColor;
            notificationText.text = "LEVEL LOCKED\nNeed " + shardsLeft + " more shards!";
            
            notificationText.transform.parent.gameObject.SetActive(true);

            yield return new WaitForSeconds(displayDuration);

            notificationText.transform.parent.gameObject.SetActive(false);
        }

        isDisplaying = false;
    }
}