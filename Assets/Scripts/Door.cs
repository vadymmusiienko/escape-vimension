using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform doorToRotate;
    public float openAngle = 90f;
    public float rotationSpeed = 2f;
    public string playerTag = "Player";
    public KeyCode interactionKey = KeyCode.E;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool playerIsNear = false;
    private bool isOpen = false;

    // 可以在Start方法中记录门的初始旋转
    void Start()
    {
        if (doorToRotate != null)
        {
            initialRotation = doorToRotate.rotation;
        }
    }

    void Update()
    {
        // 如果玩家在范围内并且按下交互键
        if (playerIsNear && Input.GetKeyDown(interactionKey))
        {
            // 切换门的开/关状态
            isOpen = !isOpen;
            Debug.Log("Open/Close");
        }

        // 根据isOpen状态，计算目标旋转
        if (doorToRotate != null)
        {
            if (isOpen)
            {
                // 计算开门的目标旋转
                targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
            }
            else
            {
                // 关门的目标旋转
                targetRotation = initialRotation;
            }

            // 平滑地旋转到目标角度
            doorToRotate.rotation = Quaternion.Slerp(doorToRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // 检测玩家进入触发器
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerIsNear = true;
            Debug.Log("按 " + interactionKey.ToString() + " 开/关门。");
        }
    }

    // 检测玩家离开触发器
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerIsNear = false;
        }
    }
}