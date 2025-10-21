using UnityEngine;

/// <summary>
/// Handles teleportation when player falls into a trap.
/// This component works independently of dialogue and persists after first use.
/// </summary>
public class TrapTeleportTrigger : MonoBehaviour
{
    [Header("Teleportation Settings")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private bool teleportOnTrigger = true;
    
    [Header("Dialogue Integration")]
    [SerializeField] private bool waitForDialogueOnFirstTime = true;
    
    
    private bool hasTriggeredBefore = false;
    private DialogueTrigger dialogueTrigger;
    
    private void Awake()
    {
        // Get the dialogue trigger component on this GameObject
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If this is the first time and we should wait for dialogue
            if (!hasTriggeredBefore && waitForDialogueOnFirstTime && dialogueTrigger != null)
            {
                // Don't teleport immediately, let dialogue handle it
                hasTriggeredBefore = true;
                return;
            }
            
            // Teleport immediately (subsequent times or no dialogue)
            if (teleportOnTrigger)
            {
                TeleportPlayer();
            }
        }
    }
    
    /// <summary>
    /// Teleports the player to the respawn point
    /// </summary>
    public void TeleportPlayer()
    {
        if (respawnPoint == null)
        {
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        // Disable character controller to allow position change
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Teleport player
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        // Re-enable character controller
        if (controller != null)
        {
            controller.enabled = true;
        }
    }
    
    /// <summary>
    /// Sets the respawn point for teleportation
    /// </summary>
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
    
    /// <summary>
    /// Enables or disables automatic teleportation on trigger
    /// </summary>
    public void SetTeleportOnTrigger(bool enabled)
    {
        teleportOnTrigger = enabled;
    }
    
    /// <summary>
    /// Called by dialogue system when dialogue finishes
    /// This allows the dialogue to handle teleportation on first time
    /// </summary>
    public void OnDialogueFinished()
    {
        TeleportPlayer();
    }
    
    /// <summary>
    /// Resets the trigger state (useful for testing or resetting the trap)
    /// </summary>
    public void ResetTriggerState()
    {
        hasTriggeredBefore = false;
    }
}
