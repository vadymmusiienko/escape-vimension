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
                Debug.Log("Entered");
                if (playerScript.HasItem(keyName))
                {
                    Debug.Log("Has key");
                }
                if (!requiresKey || playerScript.HasItem(keyName))
                {
                    Door.Open(other.transform.position);
                    PlayDoorSound();
                    canOpen = false;
                }
                else
                {
                    Debug.Log("It's locked, you need a " +  keyName);
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
