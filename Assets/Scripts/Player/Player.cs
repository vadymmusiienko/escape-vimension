using UnityEngine;

public class Player : Entity
{
    [Header("Components")]
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerInteraction interaction;
    public PlayerLeveling leveling;
    
    [Header("Camera")]
    public CameraFollow cameraFollow;
    
    // State Machine
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    
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
    
    protected override void Awake()
    {
        base.Awake();
        
        // Get references to manually assigned components
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        interaction = GetComponent<PlayerInteraction>();
        leveling = GetComponent<PlayerLeveling>();
        
        // Initialize state machine
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }
    
    protected override void Start()
    {
        base.Start();
        
        // Setup camera
        Camera cam = Camera.main;
        if (cam != null)
        {
            cameraFollow = cam.GetComponent<CameraFollow>();
            if (cameraFollow == null)
            {
                cameraFollow = cam.gameObject.AddComponent<CameraFollow>();
            }
            cameraFollow.SetTarget(transform);
        }
        
        stateMachine.Initialize(idleState);
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
    
    // Camera control methods
    public void SetCameraOffset(Vector3 offset)
    {
        cameraFollow?.SetOffset(offset);
    }
    
    public void SetCameraFollowSpeed(float speed)
    {
        cameraFollow?.SetFollowSpeed(speed);
    }
    
    public void SnapCameraToPlayer()
    {
        cameraFollow?.SnapToTarget();
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
            Debug.Log($"Added {itemName} to inventory");
        }
    }
    
    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"Removed {itemName} from inventory");
        }
    }
    
    public System.Collections.Generic.List<string> GetInventory()
    {
        return new System.Collections.Generic.List<string>(inventory);
    }
}