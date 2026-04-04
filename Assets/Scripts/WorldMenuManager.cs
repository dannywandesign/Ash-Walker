using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldMenuManager : MonoBehaviour
{
    public Button[] worldButtons; 

    void Start()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None; 

        RefreshButtons();
    }

    public void RefreshButtons()
    {
        for (int i = 0; i < worldButtons.Length; i++)
        {
            bool isUnlocked = GameSession.WorldUnlocked[i];

            worldButtons[i].interactable = isUnlocked;

            Image img = worldButtons[i].GetComponent<Image>();
            if (img != null)
            {
                img.color = isUnlocked ? Color.white : new Color(1, 1, 1, 0.4f);
            }
        }
    }

    public void LoadLevel(string sceneName)
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneName);
    }

    public void GoBackToStart()
    {
        SceneManager.LoadScene("StartScreen");
    }
}