using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashDuration;
    private float dashDistance;
    private int dashMultiplier;
    private Vector2 dashDirection;
    private Vector3 dashStartPosition;
    private Vector3 dashTargetPosition;
    private bool isDashing = false;
    private CharacterController characterController;
    
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName) 
    {
        dashDuration = 0.3f; // Default dash duration
        dashDistance = 1f;   // Default dash distance
        characterController = player.GetComponent<CharacterController>();
    }
    
    public void InitializeDash(int multiplier, Vector2 direction, float duration, float distance)
    {
        dashMultiplier = multiplier;
        dashDirection = direction;
        dashDistance = distance;
        dashStartPosition = player.transform.position;
        
        // Calculate duration based on multiplier: longer dashes take longer time
        dashDuration = duration * dashMultiplier;
        
        // Calculate target position
        float totalDashDistance = dashDistance * dashMultiplier;
        dashTargetPosition = dashStartPosition + new Vector3(
            dashDirection.x * totalDashDistance,
            0, // No vertical movement during dash
            dashDirection.y * totalDashDistance
        );
        
        isDashing = true;
        stateTimer = dashDuration;
        
        // Disable gravity during dash
        if (player.movement != null)
        {
            player.movement.SetGravityEnabled(false);
        }
        
        // Keep CharacterController enabled to maintain collision detection
        // We'll use CharacterController.Move() for smooth movement
    }
    
    public override void Enter()
    {
        // Don't call base.Enter() to avoid setting animation bools
        // Dash state doesn't need animation since it's a quick movement
    }
    
    public override void Exit()
    {
        // Don't call base.Exit() to avoid setting animation bools
        
        // Re-enable gravity
        if (player.movement != null)
        {
            player.movement.SetGravityEnabled(true);
        }
        
        // CharacterController stays enabled throughout dash
        
        isDashing = false;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (isDashing)
        {
            PerformDash();
        }
        
        // Check if dash is complete
        if (stateTimer <= 0)
        {
            // Return to idle state
            stateMachine.ChangeState(player.idleState);
        }
    }
    
    private void PerformDash()
    {
        // Calculate dash progress (0 to 1)
        float dashProgress = 1f - (stateTimer / dashDuration);
        
        // Use smooth interpolation for gradual movement
        Vector3 targetPosition = Vector3.Lerp(dashStartPosition, dashTargetPosition, dashProgress);
        
        // Calculate movement delta for this frame
        Vector3 currentPosition = player.transform.position;
        Vector3 movementDelta = targetPosition - currentPosition;
        
        // Use CharacterController.Move() for smooth movement with collision detection
        if (characterController != null)
        {
            characterController.Move(movementDelta);
        }
        else
        {
            // Fallback to direct position setting if no CharacterController
            player.transform.position = targetPosition;
        }
    }
}