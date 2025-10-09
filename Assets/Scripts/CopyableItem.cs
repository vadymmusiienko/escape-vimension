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
    
    [Header("Visual Feedback")]
    // TODO: change effects (set to NULL for now)
    public GameObject copyEffect; // Visual effect when copied
    public GameObject pasteEffect; // Visual effect when pasted
    
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
        
        // Show copy effect (if present)
        if (copyEffect != null)
        {
            GameObject effect = Instantiate(copyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        Debug.Log($"Copied: {itemName}");
    }
    
    public virtual void OnPasted(Player player)
    {
        isCopied = false;
        
        // Show paste effect (if not null)
        if (pasteEffect != null)
        {
            GameObject effect = Instantiate(pasteEffect, player.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        Debug.Log($"Pasted: {itemName}");
    }
    
    public GameObject CreateProjectile(Vector3 position, Vector3 direction)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"No projectile prefab assigned to {itemName}");
            return null;
        }
        
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.LookRotation(direction));
        
        // Set up projectile properties
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController == null)
        {
            projectileController = projectile.AddComponent<ProjectileController>();
        }
        
        projectileController.Initialize(direction, projectileSpeed, projectileLifetime, damage);
        
        return projectile;
    }
    
    // Visualize copy range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, copyRange);
    }
}
