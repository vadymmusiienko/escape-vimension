using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Door Door;

    [Header("Key Requirement")]
    public bool requiresKey = true;
    public string keyName = "Key";
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip doorSound; // Single sound for both opening and closing

    private bool canOpen = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player playerScript))
        {
            if (!Door.IsOpen && canOpen)
            {
                if (playerScript.HasItem(keyName))
                {
                }
                if (!requiresKey || playerScript.HasItem(keyName))
                {
                    Door.Open(other.transform.position);
                    PlayDoorSound();
                    canOpen = false;
                }
                else
                {
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player playerScript))
        {
            if (Door.IsOpen)
            {
                Door.Close();
                PlayDoorSound();
                canOpen = true;
            }
        }
    }
    
    /// <summary>
    /// Plays the door sound effect (for both opening and closing)
    /// </summary>
    private void PlayDoorSound()
    {
        if (audioSource != null && doorSound != null)
        {
            audioSource.clip = doorSound;
            audioSource.Play();
        }
    }
}
