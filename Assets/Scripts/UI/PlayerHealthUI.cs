using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        Debug.Log("HealthBar UI is ENABLED, subscribing to OnHealthChanged event.");
        PlayerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        Debug.Log("HealthBar UI is DISABLED, unsubscribing from OnHealthChanged event.");
        PlayerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        Debug.Log($"--- HealthBar UI has received an update! New Health: {currentHealth} ---");
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
