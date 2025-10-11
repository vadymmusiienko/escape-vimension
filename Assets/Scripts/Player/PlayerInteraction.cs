using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 2f;
    public LayerMask itemLayer;
    
    [Header("Copy/Paste Settings")]
    public float copyRange = 2f;
    public LayerMask copyableLayer;
    public LayerMask enemyLayer = -1;
    public float enemyDetectionRange = 10f;
    
    // State
    private Item nearbyItem;
    private CopyableItem nearbyCopyableItem;
    public CopyableItem clipboardItem;
    public bool isPickingUp = false;
    public bool isCopying = false;
    
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        DetectNearbyItems();
        DetectNearbyCopyableItems();
        
        HandlePickupInput();
        HandleCopyInput();
        HandlePasteInput();
    }
    
    private void HandlePickupInput()
    {
        if (Input.GetKeyDown(KeyCode.X) && nearbyItem != null && !isPickingUp)
        {
            PerformPickup();
        }
    }
    
    private void HandleCopyInput()
    {
        if (Input.GetKeyDown(KeyCode.Y) && nearbyCopyableItem != null && !isCopying)
        {
            PerformCopy();
        }
    }
    
    private void HandlePasteInput()
    {
        if (Input.GetKeyDown(KeyCode.P) && clipboardItem != null && !isCopying)
        {
            PerformPaste();
        }
    }
    
    private void DetectNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);
        
        if (hitColliders.Length > 0)
        {
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
    
    private void DetectNearbyCopyableItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, copyRange, copyableLayer);
        
        if (hitColliders.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            CopyableItem closestItem = null;
            
            foreach (Collider col in hitColliders)
            {
                CopyableItem item = col.GetComponent<CopyableItem>();
                if (item != null && item.CanBeCopied(GetComponent<Player>()))
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
    
    private void PerformPickup()
    {
        if (nearbyItem == null) return;
        
        isPickingUp = true;
        anim.SetTrigger("Pickup");
        
        // Pick up immediately
        nearbyItem.OnPickup(GetComponent<Player>());
        nearbyItem = null;
        
        // Reset pickup state
        Invoke(nameof(EndPickup), 1f);
    }
    
    private void EndPickup()
    {
        isPickingUp = false;
    }
    
    private void PerformCopy()
    {
        if (nearbyCopyableItem == null) return;
        
        isCopying = true;
        clipboardItem = nearbyCopyableItem;
        clipboardItem.OnCopied(GetComponent<Player>());
        
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
        
        // Get camera reference for input-based direction
        Camera cam = Camera.main;
        if (cam == null) cam = FindFirstObjectByType<Camera>();
        
        // Find nearest enemy or use input direction
        Transform nearestEnemy = FindNearestEnemy();
        Vector3 direction;
        
        if (nearestEnemy != null)
        {
            direction = (nearestEnemy.position - transform.position).normalized;
        }
        else if (cam != null)
        {
            // Calculate direction based on current input (H, J, K, L keys)
            float inputX = 0;
            float inputY = 0;
            
            if (Input.GetKey(KeyCode.H)) inputX = -1;
            if (Input.GetKey(KeyCode.L)) inputX = 1;
            if (Input.GetKey(KeyCode.J)) inputY = -1;
            if (Input.GetKey(KeyCode.K)) inputY = 1;
            
            if (inputX != 0 || inputY != 0)
            {
                // Use camera-relative direction based on input
                Vector3 forward = cam.transform.forward;
                Vector3 right = cam.transform.right;
                
                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();
                
                direction = (forward * inputY + right * inputX).normalized;
            }
            else
            {
                // No input, use camera forward
                direction = cam.transform.forward;
            }
        }
        else
        {
            // Fallback to player forward
            direction = transform.forward;
        }
        
        // Ensure direction is valid
        if (direction.magnitude < 0.001f)
        {
            direction = Vector3.forward;
        }
        
        // Spawn projectile slightly in front of player
        Vector3 spawnPosition = transform.position + Vector3.up * 1.5f + direction * 0.5f;
        
        // Create and launch projectile
        GameObject projectile = clipboardItem.CreateProjectile(spawnPosition, direction);
        
        if (projectile != null)
        {
            clipboardItem.OnPasted(GetComponent<Player>());
        }
        
        clipboardItem = null;
        Invoke(nameof(EndCopy), 0.5f);
    }
    
    private Transform FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyDetectionRange, enemyLayer);
        
        if (hitColliders.Length == 0) return null;
        
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
    
}
