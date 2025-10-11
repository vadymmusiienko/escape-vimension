using UnityEngine;

public class ExperiencePotionItem : Item
{
    [Header("Experience Potion Settings")]
    public int expAmount = 25; // How much experience this potion gives
    
    [Header("Visual Effects")]
    public GameObject pickupEffect; // Visual effect when picked up
    public GameObject expEffect; // Visual effect when experience is gained
    
    void Start()
    {
        itemName = "Experience Potion";
        canPickup = true;
    }
    
    public override void OnPickup(Player player)
    {
        base.OnPickup(player);
        
        // Show pickup effect
        if (pickupEffect != null)
        {
            GameObject effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        // Give experience to player
        if (player != null)
        {
            player.AddExperience(expAmount);
            
            // Show experience effect on player
            if (expEffect != null)
            {
                GameObject effect = Instantiate(expEffect, player.transform.position + Vector3.up, Quaternion.identity);
                Destroy(effect, 3f);
            }
            
            Debug.Log($"Experience Potion consumed! +{expAmount} EXP!");
        }
        
        // Destroy the potion after pickup
        Destroy(gameObject);
    }
    
    // Visualize the potion in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
