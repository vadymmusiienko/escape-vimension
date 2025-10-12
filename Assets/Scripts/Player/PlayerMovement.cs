using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float desiredRotationSpeed = 0.1f;
    
    [Header("Gravity")]
    public float gravity = 9.8f;
    public float groundedGravity = -2f;
    
    // Input
    public float InputX { get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }
    
    // Movement
    public Vector3 desiredMoveDirection;
    public float verticalVel;
    public bool isGrounded;
    
    // Components
    public CharacterController controller;
    private Camera cam;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
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
        if (Speed > 0)
        {
            // Calculate movement direction relative to camera
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            desiredMoveDirection = forward * InputY + right * InputX;
            
            // Rotate player to face movement direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(desiredMoveDirection),
                desiredRotationSpeed
            );
            
            // Move player
            Vector3 horizontalMove = desiredMoveDirection * Time.deltaTime * moveSpeed;
            controller.Move(horizontalMove);
        }
    }
    
    private void HandleInput()
    {
        // Handle input directly (fallback if InputManager not available)
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
    
}
