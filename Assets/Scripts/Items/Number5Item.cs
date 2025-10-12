using UnityEngine;

public class Number5Item : Item
{
    [Header("Number 5 Settings")]
    public string unlockMessage = "You found the number 5! Dash ability unlocked!";
    
    public override void OnPickup(Player player)
    {
        // Add the number 5 to inventory
        player.AddItem("Number 5");
        
        // Enable dash ability
        player.UnlockDash();
        
        // Show unlock message
        Debug.Log(unlockMessage);
        
        // You could also show a UI message here
        // For example: UIManager.Instance.ShowMessage(unlockMessage);
        
        // Destroy the object
        Destroy(gameObject);
    }
}
