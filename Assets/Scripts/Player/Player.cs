using UnityEngine;

public class Player : Entity
{
    [Header("Components")]
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerInteraction interaction;
    public PlayerLeveling leveling;
    public PlayerDashInput dashInput;
    
    
    // State Machine
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerDashState dashState;
    
    // Properties for state machine compatibility
    public float InputX => movement?.InputX ?? 0f;
    public float InputY => movement?.InputY ?? 0f;
    public float Speed => movement?.Speed ?? 0f;
    public bool isGrounded => movement?.isGrounded ?? false;
    public bool isPickingUp => interaction?.isPickingUp ?? false;
    public bool isCopying => interaction?.isCopying ?? false;
    public CopyableItem clipboardItem => interaction?.clipboardItem;
    
    // Inventory system
    private System.Collections.Generic.List<string> inventory = new System.Collections.Generic.List<string>();
    
    // Dash unlock system
    private bool dashUnlocked = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        // Get references to manually assigned components
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        interaction = GetComponent<PlayerInteraction>();
        leveling = GetComponent<PlayerLeveling>();
        dashInput = GetComponent<PlayerDashInput>();
        
        // Initialize state machine
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }
    
    protected override void Start()
    {
        base.Start();
        
        // Initialize state machine
        stateMachine.Initialize(idleState);
        
        // Setup dash input event
        if (dashInput != null)
        {
            dashInput.OnDashInput += HandleDashInput;
        }
    }
    
    protected override void Update()
    {
        base.Update();
        stateMachine.currState.Update();
    }
    
    // Delegation methods for backward compatibility
    public void AddExperience(int expAmount)
    {
        leveling?.AddExperience(expAmount);
    }
    
    public int GetCurrentStrength()
    {
        return leveling?.GetCurrentStrength() ?? 10;
    }
    
    public int GetCurrentLevel()
    {
        return leveling?.GetCurrentLevel() ?? 1;
    }
    
    public int GetCurrentExp()
    {
        return leveling?.GetCurrentExp() ?? 0;
    }
    
    public int GetExpToNextLevel()
    {
        return leveling?.GetExpToNextLevel() ?? 100;
    }
    
    
    // Inventory methods
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
    
    public void AddItem(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
        }
    }
    
    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
        }
    }
    
    public System.Collections.Generic.List<string> GetInventory()
    {
        return new System.Collections.Generic.List<string>(inventory);
    }
    
    // Dash unlock methods
    public void UnlockDash()
    {
        dashUnlocked = true;
    }
    
    public bool IsDashUnlocked()
    {
        return dashUnlocked;
    }
    
    // Dash system
    private void HandleDashInput(int multiplier, Vector2 direction)
    {
        // Check if dash is unlocked
        if (!dashUnlocked)
        {
            return;
        }
        
        // Check if we can dash (not already dashing)
        if (stateMachine.currState == dashState)
        {
            return;
        }
        
        // Initialize and start dash with size-scaled distance
        float effectiveDashDistance = dashInput.GetEffectiveDashDistance();
        dashState.InitializeDash(multiplier, direction, dashInput.dashDuration, effectiveDashDistance);
        stateMachine.ChangeState(dashState);
        
    }
}