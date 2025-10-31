using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public bool canPickup = true;
    
    // Events for dialogue triggers
    protected List<PickupDialogueTrigger> dialogueTriggers = new List<PickupDialogueTrigger>();
    
    void Awake()
    {
        // Find all PickupDialogueTrigger components on this GameObject
        dialogueTriggers.AddRange(GetComponents<PickupDialogueTrigger>());
    }
    
    // Called when the item is picked up
    public virtual void OnPickup(Player player)
    {
        // Check if there are any dialogue triggers
        bool hasDialogueTriggers = dialogueTriggers.Count > 0 && dialogueTriggers.Any(t => t != null);
        
        if (hasDialogueTriggers)
        {
            // Add item to inventory first
            player.AddItem(itemName);
            
            // Make item invisible but keep GameObject alive for dialogue
            SetItemInvisible();
            
            // Trigger dialogue
            foreach (var trigger in dialogueTriggers)
            {
                if (trigger != null)
                {
                    trigger.OnItemPickedUp(player);
                }
            }
            // Don't destroy - let the dialogue trigger handle it
        }
        else
        {
            // No dialogue triggers, proceed normally
            player.AddItem(itemName);
            Destroy(gameObject);
        }
    }
    
    protected void SetItemInvisible()
    {
        // Disable all renderers to make item invisible
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
        
        // Disable colliders so player can't interact with it again
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        
        // Optionally disable the Item component to prevent re-pickup
        this.enabled = false;
    }
}
