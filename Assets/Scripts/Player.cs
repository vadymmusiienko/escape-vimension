using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;
    public CharacterController controller {  get; private set; }
    public Camera cam { get; private set; }
    public float InputX {  get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }
    public List<string> inventory = new List<string>();

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
        
        // Handle pickup input
        if (Input.GetKeyDown(KeyCode.X) && nearbyItem != null && !isPickingUp)
        {
            PerformPickup();
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
    
    
    // Visualize pickup range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }

    public void AddItem(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
            Debug.Log($"Added {itemName} to inventory.");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}
