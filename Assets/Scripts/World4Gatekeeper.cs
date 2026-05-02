using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class World4Gatekeeper : MonoBehaviour
{
    [Header("Requirement Settings")]
    public int totalScrollsNeeded = 4;
    public string[] scrollIDs; 
    
    [Header("UI References")]
    public TextMeshProUGUI scrollCountText;
    public Button world4Button;
    public string world4SceneName = "World4";

    private int collectedCount = 0;

    void Start()
    {
        CheckScrolls();
    }

void CheckScrolls()
    {
        collectedCount = 0;

        foreach (string id in scrollIDs)
        {
            if (PlayerPrefs.GetInt(id, 0) == 1)
            {
                collectedCount++;
            }
        }

        if (scrollCountText != null)
        {
            scrollCountText.text = "Scrolls: " + collectedCount + " / " + totalScrollsNeeded;
        }

        bool isW4Unlocked = PlayerPrefs.GetInt("World4_Unlocked", 0) == 1;

        if (isW4Unlocked && collectedCount >= totalScrollsNeeded)
        {
            world4Button.interactable = true;
            scrollCountText.color = Color.green; 
        }
        else
        {
            world4Button.interactable = false;
            scrollCountText.color = Color.white; 
        }
    }

    public void TryEnterWorld4()
    {
        SceneManager.LoadScene(world4SceneName);
    }
}