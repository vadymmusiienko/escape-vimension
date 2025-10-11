using UnityEngine;

public class CopyableItem : Item
{
    [Header("Copy Settings")]
    public float copyRange = 2f; // How close player needs to be to copy this item
    public LayerMask copyableLayer; // Layer for copyable items
    
    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Prefab to instantiate when pasted
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;
    public int damage = 10;
    
    
    private bool isCopied = false;
    
    public override void OnPickup(Player player)
    {
        // Don't destroy copyable items when picked up normally
        // They should only be copied, not picked up
        Debug.Log($"Cannot pick up copyable item: {itemName}. Use Y to copy instead.");
    }
    
    public bool CanBeCopied(Player player)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= copyRange && !isCopied;
    }
    
    public virtual void OnCopied(Player player)
    {
        isCopied = true;
    }
    
    public virtual void OnPasted(Player player)
    {
        isCopied = false;
    }
    
    public GameObject CreateProjectile(Vector3 position, Vector3 direction)
    {
        if (projectilePrefab == null)
        {
            return null;
        }
        
        // Ensure direction is valid
        if (direction.magnitude < 0.001f)
        {
            direction = Vector3.forward;
        }
        
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.LookRotation(direction));
        
        // Set up projectile properties
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController == null)
        {
            projectileController = projectile.AddComponent<ProjectileController>();
        }
        
        // Initialize with proper settings for fast movement
        projectileController.Initialize(direction, projectileSpeed, projectileLifetime, damage);
        
        return projectile;
    }
    
}
