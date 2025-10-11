using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public bool canPickup = true;
    
    // Called when the item is picked up
    public virtual void OnPickup(Player player)
    {
        // TODO: logic for picking up an item
        Debug.Log($"Picked up: {itemName}");

        player.AddItem(itemName);
        // Disable or destroy the item
        gameObject.SetActive(false);
        // Or: Destroy(gameObject);
    }
    
    // Optional: Draw a sphere in the editor to show the item
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
