using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    public void TeleportPlayer()
    {
        if (respawnPoint == null)
        {
        }

        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
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

    }
}
