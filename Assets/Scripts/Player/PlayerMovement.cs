using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseMoveSpeed = 5f; // Base movement speed (for size 1.0)
    public float desiredRotationSpeed = 0.1f;
    
    [Header("Gravity")]
    public float gravity = 9.8f;
    public float groundedGravity = -2f;
    
    // Input
    public float InputX { get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }
    
    // Properties
    public float CurrentMoveSpeed => GetEffectiveMoveSpeed();
    
    // Movement
    public Vector3 desiredMoveDirection;
    public float verticalVel;
    public bool isGrounded;
    
    // Components
    public CharacterController controller;
    private LevelSystem levelSystem;
    
    // Gravity control
    private bool gravityEnabled = true;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        levelSystem = GetComponent<LevelSystem>();
    }
    
    /// <summary>
    /// Gets the effective movement speed based on player size
    /// </summary>
    private float GetEffectiveMoveSpeed()
    {
        if (levelSystem != null)
        {
            float currentSize = levelSystem.GetCurrentSize();
            return baseMoveSpeed * currentSize;
        }
        return baseMoveSpeed; // Fallback if no level system
    }
    
    void Update()
    {
        // Only handle gravity automatically
        // Input and movement are handled by the state machine system
        HandleGravity();
    }
    
    // Public method to be called by state machine
    public void UpdateMovement()
    {
        HandleMovement();
    }
    
    public void HandleMovement()
    {
        // Don't allow movement if locked
        if (IsMovementLocked())
        {
            return;
        }
        
        if (Speed > 0)
        {
            // For bird's eye view, use world space movement
            // InputX = left/right (X axis), InputY = forward/back (Z axis)
            desiredMoveDirection = new Vector3(InputX, 0, InputY);
            desiredMoveDirection.Normalize();
            
            // Rotate player to face movement direction
            if (desiredMoveDirection.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(desiredMoveDirection),
                    desiredRotationSpeed
                );
            }
            
            // Move player with size-proportional speed
            float effectiveSpeed = GetEffectiveMoveSpeed();
            Vector3 horizontalMove = desiredMoveDirection * Time.deltaTime * effectiveSpeed;
            controller.Move(horizontalMove);
        }
    }
    
    private void HandleInput()
    {
        // Handle input directly
        InputX = 0;
        InputY = 0;
        
        if (Input.GetKey(KeyCode.H)) InputX = -1;
        if (Input.GetKey(KeyCode.L)) InputX = 1;
        if (Input.GetKey(KeyCode.J)) InputY = -1;
        if (Input.GetKey(KeyCode.K)) InputY = 1;
        
        Speed = Mathf.Abs(InputX) + Mathf.Abs(InputY);
    }
    
    private void HandleGravity()
    {
        // Update ground detection using CharacterController
        isGrounded = controller.isGrounded;
        
        if (!gravityEnabled)
        {
            // Gravity disabled - don't apply any vertical movement
            return;
        }
        
        if (isGrounded)
        {
            verticalVel = groundedGravity;
        }
        else
        {
            verticalVel -= gravity * Time.deltaTime;
        }
        
        Vector3 gravityMove = new Vector3(0, verticalVel * Time.deltaTime, 0);
        controller.Move(gravityMove);
    }
    
    public void SetGravityEnabled(bool enabled)
    {
        gravityEnabled = enabled;
        if (!enabled)
        {
            verticalVel = 0; // Reset vertical velocity when disabling gravity
        }
    }
    
    public bool IsMovementLocked()
    {
        return IsDialogueActive();
    }
    
    private bool IsDialogueActive()
    {
        return DialogueManager.instance != null && DialogueManager.instance.IsDialogueActive();
    }
    
}
