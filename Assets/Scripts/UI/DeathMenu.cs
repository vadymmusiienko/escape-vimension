using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour 
{
    public void RestartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}