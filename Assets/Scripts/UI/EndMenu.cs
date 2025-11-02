using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void End()
    {
        SceneManager.LoadScene("EndScene");
    }
}
