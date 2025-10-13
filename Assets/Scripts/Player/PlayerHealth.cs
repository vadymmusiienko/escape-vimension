using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float deathDelay = 2f; // Timeout before loading death scene

    public float CurrentHealth {  get; private set; }

    public static event Action<float, float> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0f);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        Debug.Log($"Player takes {damage} damage, current health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Game Over!");
        // TODO: add effect of death

        // Use Invoke instead of coroutine for more reliable timing
        Debug.Log($"Starting death scene with delay: {deathDelay} seconds");
        Invoke(nameof(LoadDeathScene), deathDelay);
    }

    private void LoadDeathScene()
    {
        Debug.Log("Delay finished, loading DeathScene...");
        gameObject.SetActive(false);
        SceneManager.LoadScene("DeathScene");
    }
}
