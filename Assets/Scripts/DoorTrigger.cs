using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Door Door;

    [Header("Key Requirement")]
    public bool requiresKey = true;
    public string keyName = "Key";

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
                canOpen = true;
            }
        }
    }
}
