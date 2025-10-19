using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        SceneManager.LoadScene("EndScene");
    }
}
