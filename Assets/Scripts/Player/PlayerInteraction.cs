using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float basePickupRange = 2f; // Base pickup range for size 1.0
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
    private LevelSystem levelSystem;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        levelSystem = GetComponent<LevelSystem>();
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
        float effectivePickupRange = GetEffectivePickupRange();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectivePickupRange, itemLayer);
        
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
        
        // Play pickup sound
        PlayerAudioController audioController = GetComponent<PlayerAudioController>();
        if (audioController != null)
        {
            audioController.PlayPickupSound();
        }
        
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
        
        // Find nearest enemy or use mouse cursor or input direction
        Transform nearestEnemy = FindNearestEnemy();
        Vector3 direction;
        
        if (nearestEnemy != null)
        {
            // Priority 1: Target nearest enemy
            direction = (nearestEnemy.position - transform.position).normalized;
        }
        else if (cam != null)
        {
            // Priority 2: Target mouse cursor position
            Vector3 mouseWorldPos = GetMouseWorldPosition(cam);
            if (mouseWorldPos != Vector3.zero)
            {
                direction = (mouseWorldPos - transform.position).normalized;
            }
            else
            {
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
    
    private Vector3 GetMouseWorldPosition(Camera cam)
    {
        // Get mouse position in screen coordinates
        Vector3 mouseScreenPos = Input.mousePosition;
        
        // For top-down view, we need to project the mouse position onto the ground plane
        // Create a ray from the camera through the mouse position
        Ray ray = cam.ScreenPointToRay(mouseScreenPos);
        
        // Create a plane at the player's height (ground level)
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        
        // Check if the ray intersects with the ground plane
        if (groundPlane.Raycast(ray, out float distance))
        {
            // Get the world position where the ray hits the ground
            Vector3 worldPos = ray.GetPoint(distance);
            return worldPos;
        }
        
        // If no intersection, return zero vector (will fall back to keyboard input)
        return Vector3.zero;
    }
    
    /// <summary>
    /// Gets the effective pickup range based on player size
    /// </summary>
    private float GetEffectivePickupRange()
    {
        if (levelSystem != null)
        {
            float playerSize = levelSystem.GetCurrentSize();
            return basePickupRange * playerSize;
        }
        return basePickupRange; // Fallback if no level system
    }
    
}
