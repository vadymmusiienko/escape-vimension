using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    public void TeleportPlayer()
    {
        Debug.Log("Trying to teleport");
        if (respawnPoint == null)
        {
            Debug.Log("Respawn point does not set up.");
        }

        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.Log("Cannot find player");
            return;
        }

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Teleported.");
    }
}
