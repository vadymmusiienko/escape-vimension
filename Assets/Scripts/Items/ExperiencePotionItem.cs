using UnityEngine;

public class ExperiencePotionItem : Item
{
    [Header("Experience Potion Settings")]
    public int expAmount = 25; // How much experience this potion gives
    
    
    void Start()
    {
        itemName = "Experience Potion";
        canPickup = true;
    }
    
    public override void OnPickup(Player player)
    {
        if (player != null)
        {
            player.AddExperience(expAmount);
        }
        
        base.OnPickup(player);
    }
    
}
