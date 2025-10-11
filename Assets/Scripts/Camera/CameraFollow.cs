using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The player to follow
    
    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset from target
    public float followSpeed = 5f; // How fast the camera follows
    public float rotationSpeed = 2f; // How fast the camera rotates
    
    [Header("Smoothing Settings")]
    public bool smoothFollow = true; // Enable smooth following
    public float smoothTime = 0.3f; // Smoothing time for position
    
    // Private variables for smooth following
    private Vector3 velocity = Vector3.zero;
    
    // Cached components
    private Camera cam;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        
        // If no target is assigned, try to find the player
        if (target == null)
        {
            Player player = Object.FindFirstObjectByType<Player>();
            if (player != null)
            {
                target = player.transform;
            }
        }
        
        // Set initial position
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Calculate target position
        Vector3 targetPosition = target.position + offset;
        
        // Move camera to target position
        if (smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        
        // Make camera look at target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    
    // Public methods for external control
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
    
    public void SetFollowSpeed(float speed)
    {
        followSpeed = speed;
    }
    
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
    
    // Method to instantly snap to target (useful for cutscenes or respawning)
    public void SnapToTarget()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
