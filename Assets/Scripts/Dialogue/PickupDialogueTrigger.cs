using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Triggers dialogue when a pickable item is picked up.
/// Attach this component to any GameObject with an Item component.
/// 
/// Usage:
/// 1. Add this component to a pickable item
/// 2. Configure the dialogue strings in the Inspector
/// 3. Set whether to delete after dialogue and trigger only once
/// 4. When player picks up the item, dialogue will trigger
/// </summary>
public class PickupDialogueTrigger : MonoBehaviour, IDialogueTrigger
{
    [Header("Dialogue Settings")]
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private bool deleteAfterDialogue = true;
    
    [Header("Item Settings")]
    [SerializeField] private bool triggerOnlyOnce = true;
    
    private bool hasTriggered = false;
    private Item itemComponent;
    
    void Awake()
    {
        // Get the Item component on this GameObject
        itemComponent = GetComponent<Item>();
        
        // If no Item component exists, add one
        if (itemComponent == null)
        {
            itemComponent = gameObject.AddComponent<Item>();
        }
    }
    
    void Start()
    {
        // Subscribe to the item pickup event
        if (itemComponent != null)
        {
            // We'll override the OnPickup method to trigger dialogue
        }
    }
    
    // This method will be called by the Item component when picked up
    public void OnItemPickedUp(Player player)
    {
        // Check if we should trigger (only once if specified)
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }
        
        // Trigger dialogue
        if (DialogueManager.instance != null && dialogueStrings.Count > 0)
        {
            DialogueManager.instance.DialogueStart(dialogueStrings, this);
            hasTriggered = true;
        }
    }
    
    // This method will be called by DialogueManager when dialogue ends
    public void DeleteTrigger()
    {
        if (deleteAfterDialogue)
        {
            // Destroy the entire GameObject (item) after dialogue
            Destroy(gameObject);
        }
        else
        {
            // Just destroy this component if not deleting after dialogue
            Destroy(this);
        }
    }
    
}
