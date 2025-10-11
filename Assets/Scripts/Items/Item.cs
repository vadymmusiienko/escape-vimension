using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public bool canPickup = true;
    
    // Called when the item is picked up
    public virtual void OnPickup(Player player)
    {
        Destroy(gameObject);
    }
}
