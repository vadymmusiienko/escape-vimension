using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;
    public CharacterController controller {  get; private set; }
    public Camera cam { get; private set; }
    public CameraFollow cameraFollow { get; private set; }
    public float InputX {  get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }

    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public float allowPlayerRotation = -999f;

    public float verticalVel;
    public bool isGrounded;
    
    // Attack system
    private float lastAttackTime = 0f;
    public float attackCooldown = 1.5f;
    
    // TODO: Pickup system
    public float pickupRange = 2f; // How close player needs to be to pick up items
    public LayerMask itemLayer; // Set this in Inspector to detect only items
    private Item nearbyItem; // The item we're currently near
    public bool isPickingUp = false; // Made public so states can check it
    
    // Copy/Paste system
    public float copyRange = 2f; // How close player needs to be to copy items
    public LayerMask copyableLayer; // Set this in Inspector to detect copyable items
    public LayerMask enemyLayer = -1; // Layer mask for enemies
    public float enemyDetectionRange = 10f; // Range to detect enemies for paste targeting
    private CopyableItem nearbyCopyableItem; // The copyable item we're currently near
    public CopyableItem clipboardItem; // The item currently in clipboard (only one at a time)
    public bool isCopying = false; // Made public so states can check it
    
    // Leveling system
    [Header("Leveling System")]
    public LevelSystem levelSystem; // Reference to the level system
    public ExpUI expUI; // Reference to the experience UI

    #region States
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        controller = GetComponent<CharacterController>();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }
    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        
        // Initialize leveling system
        if (levelSystem == null)
        {
            levelSystem = GetComponent<LevelSystem>();
            if (levelSystem == null)
            {
                levelSystem = gameObject.AddComponent<LevelSystem>();
            }
        }
        
        // Connect ExpUI to LevelSystem
        if (expUI != null && levelSystem != null)
        {
            expUI.SetLevelSystem(levelSystem);
        }
        
        // Setup camera follow
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
        
        // Handle attack input independently of state
        if (Input.GetKeyDown(KeyCode.D) && CanAttack())
        {
            PerformAttack();
        }
        
        // Detect nearby items
        DetectNearbyItems();
        
        // Detect nearby copyable items
        DetectNearbyCopyableItems();
        
        // Handle pickup input
        if (Input.GetKeyDown(KeyCode.X) && nearbyItem != null && !isPickingUp)
        {
            PerformPickup();
        }
        
        // Handle copy input
        if (Input.GetKeyDown(KeyCode.Y) && nearbyCopyableItem != null && !isCopying)
        {
            PerformCopy();
        }
        
        // Handle paste input
        if (Input.GetKeyDown(KeyCode.P) && clipboardItem != null && !isCopying)
        {
            PerformPaste();
        }
        
        stateMachine.currState.Update();
    }
    
    private bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }
    
    private void PerformAttack()
    {
        // Trigger the attack animation (doesn't change state)
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        
        // Add attack logic here
        Debug.Log("Player attacking!");
        
        // TODO: Add damage detection, effects, etc.
    }
    
    private void DetectNearbyItems()
    {
        // Find all items within pickup range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);
        
        if (hitColliders.Length > 0)
        {
            // Find the closest item
            float closestDistance = Mathf.Infinity;
            Item closestItem = null;
            
            foreach (Collider col in hitColliders)
            {
                Item item = col.GetComponent<Item>();
                if (item != null && item.canPickup)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestItem = item;
                    }
                }
            }
            
            nearbyItem = closestItem;
        }
        else
        {
            nearbyItem = null;
        }
    }
    
    private void PerformPickup()
    {
        if (nearbyItem == null) return;
        
        isPickingUp = true;
        
        // Trigger the pickup animation
        anim.SetTrigger("Pickup");
        
        Debug.Log($"Picking up item: {nearbyItem.itemName}");
        
        // Store reference to item and pick it up after animation
        Item itemToPickup = nearbyItem;
        
        // Actually pick up the item after a delay (when hand reaches down in animation)
        // Adjust this delay to match animation timing
        Invoke(nameof(CompletePickup), 0.35f);
    }
    
    private void CompletePickup()
    {
        if (nearbyItem != null)
        {
            nearbyItem.OnPickup(this);
            nearbyItem = null;
        }
        
        // Set isPickingUp to false after 0.7 seconds
        Invoke(nameof(EndPickup), 1.1f);
    }
    
    private void EndPickup()
    {
        isPickingUp = false;
    }
    
    private void DetectNearbyCopyableItems()
    {
        // Find all copyable items within copy range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, copyRange, copyableLayer);
        
        if (hitColliders.Length > 0)
        {
            // Find the closest copyable item
            float closestDistance = Mathf.Infinity;
            CopyableItem closestItem = null;
            
            foreach (Collider col in hitColliders)
            {
                CopyableItem item = col.GetComponent<CopyableItem>();
                if (item != null && item.CanBeCopied(this))
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestItem = item;
                    }
                }
            }
            
            nearbyCopyableItem = closestItem;
        }
        else
        {
            nearbyCopyableItem = null;
        }
    }
    
    private void PerformCopy()
    {
        if (nearbyCopyableItem == null) return;
        
        isCopying = true;
        
        // Clear any existing clipboard item
        if (clipboardItem != null)
        {
            clipboardItem = null;
        }
        
        // Copy the item
        clipboardItem = nearbyCopyableItem;
        clipboardItem.OnCopied(this);
        
        Debug.Log($"Copied: {clipboardItem.itemName}");
        
        // Set isCopying to false after a short delay
        Invoke(nameof(EndCopy), 0.5f);
    }
    
    private void EndCopy()
    {
        isCopying = false;
    }
    
    private void PerformPaste()
    {
        if (clipboardItem == null) return;
        
        isCopying = true;
        
        // Find the nearest enemy
        Transform nearestEnemy = FindNearestEnemy();
        Vector3 direction;
        
        if (nearestEnemy != null)
        {
            // Aim at the nearest enemy
            direction = (nearestEnemy.position - transform.position).normalized;
            Debug.Log($"Pasting {clipboardItem.itemName} at enemy");
        }
        else
        {
            // Shoot straight forward
            direction = transform.forward;
            Debug.Log($"Pasting {clipboardItem.itemName} straight ahead");
        }
        
        // Create projectile
        Vector3 spawnPosition = transform.position + Vector3.up * 1.5f + direction * 1f; // Spawn at chest height and slightly in front of player
        GameObject projectile = clipboardItem.CreateProjectile(spawnPosition, direction);
        
        if (projectile != null)
        {
            clipboardItem.OnPasted(this);
        }
        
        // Clear clipboard
        clipboardItem = null;
        
        // Set isCopying to false after a short delay
        Invoke(nameof(EndCopy), 0.5f);
    }
    
    private Transform FindNearestEnemy()
    {
        // Find all enemies within detection range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyDetectionRange, enemyLayer);
        
        if (hitColliders.Length == 0) return null;
        
        // Find the closest enemy
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        
        foreach (Collider col in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = col.transform;
            }
        }
        
        return closestEnemy;
    }
    
    // Camera control methods
    public void SetCameraOffset(Vector3 offset)
    {
        if (cameraFollow != null)
        {
            cameraFollow.SetOffset(offset);
        }
    }
    
    public void SetCameraFollowSpeed(float speed)
    {
        if (cameraFollow != null)
        {
            cameraFollow.SetFollowSpeed(speed);
        }
    }
    
    public void SnapCameraToPlayer()
    {
        if (cameraFollow != null)
        {
            cameraFollow.SnapToTarget();
        }
    }
    
    // Visualize pickup and copy ranges in editor
    private void OnDrawGizmosSelected()
    {
        // Pickup range (green)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
        
        // Copy range (blue)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, copyRange);
        
        // Enemy detection range (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }
    
    #region Leveling System
    
    /// <summary>
    /// Adds experience to the player (replaces direct strength gain)
    /// </summary>
    /// <param name="expAmount">Amount of experience to add</param>
    public void AddExperience(int expAmount)
    {
        if (levelSystem != null)
        {
            levelSystem.AddExperience(expAmount);
            Debug.Log($"Experience gained: +{expAmount} EXP!");
        }
        else
        {
            Debug.LogError("LevelSystem not found! Cannot add experience.");
        }
    }
    
    /// <summary>
    /// Gets the current total strength from leveling
    /// </summary>
    /// <returns>Current total strength</returns>
    public int GetCurrentStrength()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetTotalStrength();
        }
        return 10; // Default fallback
    }
    
    /// <summary>
    /// Gets the current level
    /// </summary>
    /// <returns>Current level</returns>
    public int GetCurrentLevel()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetCurrentLevel();
        }
        return 1; // Default fallback
    }
    
    /// <summary>
    /// Gets the current experience
    /// </summary>
    /// <returns>Current experience</returns>
    public int GetCurrentExp()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetCurrentExp();
        }
        return 0; // Default fallback
    }
    
    /// <summary>
    /// Gets the experience needed for next level
    /// </summary>
    /// <returns>Experience needed for next level</returns>
    public int GetExpToNextLevel()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetExpToNextLevel();
        }
        return 100; // Default fallback
    }
    
    #endregion
}
