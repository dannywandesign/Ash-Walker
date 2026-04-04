using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool hasClearedProgressThisSession = false;

    void Awake()
    {
        if (!hasClearedProgressThisSession)
        {
            ResetAllProgress();
            hasClearedProgressThisSession = true;
            Debug.Log("New Session Started: All world progress and shards cleared.");
        }
    }

    public void GoToWorlds()
    {
        SceneManager.LoadScene("Worlds");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        
        PlayerPrefs.Save();
    }
}