using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fogCoversToControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject fog in fogCoversToControl)
            {
                if (fog != null)
                {
                    fog.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject fog in fogCoversToControl)
            {
                if (fog != null)
                {
                    fog.SetActive(true);
                }
            }
        }
    }
}
