using UnityEngine;

public class FireballItem : CopyableItem
{
    [Header("Fireball Settings")]
    public GameObject fireballProjectilePrefab;
    public float fireballSpeed = 15f;
    public float fireballLifetime = 3f;
    public int fireballDamage = 15;
    
    [Header("Fireball Effects")]
    public GameObject fireballCopyEffect;
    public GameObject fireballPasteEffect;
    
    void Start()
    {
        // Set up fireball-specific properties
        itemName = "Fireball";
        copyRange = 2f;
        projectileSpeed = fireballSpeed;
        projectileLifetime = fireballLifetime;
        damage = fireballDamage;
        
        // Assign fireball-specific effects
        if (fireballCopyEffect != null)
            copyEffect = fireballCopyEffect;
        if (fireballPasteEffect != null)
            pasteEffect = fireballPasteEffect;
        if (fireballProjectilePrefab != null)
            projectilePrefab = fireballProjectilePrefab;
    }
    
    public override void OnCopied(Player player)
    {
        base.OnCopied(player);
        Debug.Log("🔥 Fireball copied to clipboard!");
    }
    
    public override void OnPasted(Player player)
    {
        base.OnPasted(player);
        Debug.Log("🔥 Fireball pasted - launching projectile!");
    }
}
