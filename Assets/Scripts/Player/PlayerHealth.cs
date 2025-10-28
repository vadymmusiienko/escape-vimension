using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;


    public PlayerHealthUI healthbar;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthbar != null)
        {
            healthbar.UpdateHealthbar(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player health: " + currentHealth);

        // Play damage sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDamageSound();
        }

        if (healthbar != null)
        {
            healthbar.UpdateHealthbar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Invoke(nameof(LoadDeathScene), 2.0f);
    }

    private void LoadDeathScene()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("DeathScene");
    }
}