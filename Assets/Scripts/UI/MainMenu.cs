using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public void PlayGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("VideoScene");
    }
    
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

}