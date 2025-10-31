using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Triggers dialogue when an enemy dies.
/// Attach this component to any GameObject with an Enemy component.
/// 
/// Usage:
/// 1. Add this component to an enemy
/// 2. Configure the dialogue strings in the Inspector
/// 3. Set whether to delete after dialogue and trigger only once
/// 4. When the enemy dies, dialogue will trigger
/// </summary>
public class EnemyDeathDialogueTrigger : MonoBehaviour, IDialogueTrigger
{
    [Header("Dialogue Settings")]
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private bool deleteAfterDialogue = true; // Usually delete after death dialogue
    
    [Header("Enemy Settings")]
    [SerializeField] private bool triggerOnlyOnce = true;
    
    private bool hasTriggered = false;
    private Enemy enemyComponent;
    
    void Awake()
    {
        // Get the Enemy component on this GameObject
        enemyComponent = GetComponent<Enemy>();
        
        // If no Enemy component exists, log a warning
        if (enemyComponent == null)
        {
            Debug.LogWarning($"EnemyDeathDialogueTrigger on {gameObject.name} requires an Enemy component!");
        }
    }
    
    void Start()
    {
        // Subscribe to the enemy death event
        if (enemyComponent != null)
        {
            Enemy.OnEnemyDied += OnEnemyDied;
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        Enemy.OnEnemyDied -= OnEnemyDied;
    }
    
    private void OnEnemyDied(Enemy deadEnemy)
    {
        // Check if this is our enemy and we should trigger
        if (deadEnemy == enemyComponent && CanTrigger())
        {
            TriggerDialogue();
        }
    }
    
    private bool CanTrigger()
    {
        return !triggerOnlyOnce || !hasTriggered;
    }
    
    public void TriggerDialogue()
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
            // Destroy the entire GameObject (enemy) after dialogue
            Destroy(gameObject);
        }
        else
        {
            // Just destroy this component if not deleting after dialogue
            Destroy(this);
        }
    }
}
