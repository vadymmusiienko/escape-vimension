using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float attackDuration = 0.5f; // How long the attack lasts
    private float attackCooldown = 1f; // Cooldown between attacks
    private float lastAttackTime = 0f;
    private float attackMoveSpeedMultiplier = 0.3f; // How much to slow down movement during attack (0.3 = 30% speed)
    private bool wasMovingBeforeAttack = false; // Track if player was moving when attack started
    
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        
        // Track if player was moving when attack started
        wasMovingBeforeAttack = player.Speed > 0;
        
        // Set attack duration timer
        stateTimer = attackDuration;
        
        // Record attack time for cooldown
        lastAttackTime = Time.time;
        
        // Perform attack logic here
        PerformAttack();
    }

    public override void Exit() 
    { 
        base.Exit(); 
    }

    public override void Update()
    {
        base.Update();
        
        // Allow slowed-down movement during attack
        if (player.Speed > 0)
        {
            var camera = player.cam;
            var forward = camera.transform.forward;
            var right = camera.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            player.desiredMoveDirection = forward * player.InputY + right * player.InputX;

            // Rotate player to face movement direction (slower during attack)
            player.transform.rotation = Quaternion.Slerp(
                player.transform.rotation,
                Quaternion.LookRotation(player.desiredMoveDirection),
                player.desiredRotationSpeed * 0.5f // Slower rotation during attack
            );

            // Move at reduced speed
            Vector3 horizontalMove = player.desiredMoveDirection * Time.deltaTime * player.moveSpeed * attackMoveSpeedMultiplier;
            player.controller.Move(horizontalMove);
        }
        
        // Check if attack duration is over
        if (stateTimer <= 0)
        {
            // Return to appropriate state after attack
            if (player.Speed > 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }
    
    private void PerformAttack()
    {
        // Add your attack logic here
        Debug.Log("Player is attacking!");
        
        // TODO: Implement attack logic
        // Example: Deal damage to enemies in range
        // Example: Play attack sound effect
        // Example: Create attack effects/particles
        
        // - Raycast to detect enemies
        // - Apply damage to targets
        // - Play attack animations
        // - Create visual/audio effects
    }
    
    // Check if player can attack (cooldown check)
    public bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }
    
    // Get remaining cooldown time
    public float GetCooldownRemaining()
    {
        float remaining = attackCooldown - (Time.time - lastAttackTime);
        return Mathf.Max(0, remaining);
    }
}
