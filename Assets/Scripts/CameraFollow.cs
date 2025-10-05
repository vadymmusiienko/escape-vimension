using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The player to follow
    
    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset from target
    public float followSpeed = 5f; // How fast the camera follows
    public float rotationSpeed = 2f; // How fast the camera rotates
    
    [Header("Look Ahead Settings")]
    public bool lookAhead = true; // Enable look ahead feature
    public float lookAheadDistance = 2f; // How far ahead to look
    public float lookAheadSpeed = 3f; // How fast to move the look ahead
    
    [Header("Smoothing Settings")]
    public bool smoothFollow = true; // Enable smooth following
    public float smoothTime = 0.3f; // Smoothing time for position
    
    [Header("Constraints")]
    public bool constrainX = false; // Constrain camera movement on X axis
    public bool constrainY = false; // Constrain camera movement on Y axis
    public bool constrainZ = false; // Constrain camera movement on Z axis
    
    // Private variables for smooth following
    private Vector3 velocity = Vector3.zero;
    private Vector3 currentLookAhead = Vector3.zero;
    private Vector3 targetLookAhead = Vector3.zero;
    
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
        Vector3 targetPosition = CalculateTargetPosition();
        
        // Apply constraints
        targetPosition = ApplyConstraints(targetPosition);
        
        // Move camera to target position
        if (smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        
        // Handle look ahead
        if (lookAhead)
        {
            HandleLookAhead();
        }
        
        // Make camera look at target
        Vector3 lookTarget = target.position + currentLookAhead;
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    private Vector3 CalculateTargetPosition()
    {
        return target.position + offset;
    }
    
    private Vector3 ApplyConstraints(Vector3 position)
    {
        Vector3 constrainedPosition = position;
        
        if (constrainX)
            constrainedPosition.x = transform.position.x;
        if (constrainY)
            constrainedPosition.y = transform.position.y;
        if (constrainZ)
            constrainedPosition.z = transform.position.z;
            
        return constrainedPosition;
    }
    
    private void HandleLookAhead()
    {
        // Calculate look ahead based on target's movement
        Vector3 targetVelocity = target.GetComponent<CharacterController>()?.velocity ?? Vector3.zero;
        
        if (targetVelocity.magnitude > 0.1f)
        {
            targetLookAhead = targetVelocity.normalized * lookAheadDistance;
        }
        else
        {
            targetLookAhead = Vector3.zero;
        }
        
        // Smoothly transition to target look ahead
        currentLookAhead = Vector3.Lerp(currentLookAhead, targetLookAhead, lookAheadSpeed * Time.deltaTime);
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
