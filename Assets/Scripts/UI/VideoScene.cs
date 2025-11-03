using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoScene : MonoBehaviour
{
    public VideoPlayer player;
    public string nextSceneName = "MainScene";

    void Start()
    {
        if (!player) player = GetComponent<VideoPlayer>();

        player.isLooping = false;
        player.loopPointReached += _ => SceneManager.LoadScene(nextSceneName);

        player.Prepare();
        player.prepareCompleted += _ => player.Play();
    }
}
