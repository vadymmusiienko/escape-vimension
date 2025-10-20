using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Number5Item : Item
{
    [Header("Number 5 Settings")]
    public string unlockMessage = "You found the number 5! Dash ability unlocked!";
    
    public override void OnPickup(Player player)
    {
        // Check if there are any dialogue triggers
        bool hasDialogueTriggers = dialogueTriggers.Count > 0 && dialogueTriggers.Any(t => t != null);
        
        if (hasDialogueTriggers)
        {
            // Add the number 5 to inventory
            player.AddItem("Number 5");
            
            // Enable dash ability
            player.UnlockDash();
            
            // Show unlock message
            Debug.Log(unlockMessage);
            
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
            // Add the number 5 to inventory
            player.AddItem("Number 5");
            
            // Enable dash ability
            player.UnlockDash();
            
            // Show unlock message
            Debug.Log(unlockMessage);
            
            // Destroy the object
            Destroy(gameObject);
        }
    }
}
