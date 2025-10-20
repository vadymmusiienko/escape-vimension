using UnityEngine;

/// <summary>
/// Example script showing how to use the enemy health system events
/// Attach this to any GameObject to listen for enemy health changes
/// </summary>
public class HealthSystemExample : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to health system events
        Enemy.OnEnemyHealthChanged += OnEnemyHealthChanged;
        Enemy.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks
        Enemy.OnEnemyHealthChanged -= OnEnemyHealthChanged;
        Enemy.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyHealthChanged(Enemy enemy, float currentHealth, float maxHealth)
    {
        float healthPercentage = (currentHealth / maxHealth) * 100f;
        
        // You can add custom logic here, such as:
        // - Update UI elements
        // - Play sound effects
        // - Trigger special effects
        // - Update game statistics
    }

    private void OnEnemyDied(Enemy enemy)
    {
        
        // You can add custom logic here, such as:
        // - Award experience points
        // - Drop items
        // - Update kill counters
        // - Trigger death effects
    }
}
