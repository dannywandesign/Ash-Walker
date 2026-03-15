using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "MainWorld" with the exact name of your scene file
        SceneManager.LoadScene("MainWorld"); 
    }
}