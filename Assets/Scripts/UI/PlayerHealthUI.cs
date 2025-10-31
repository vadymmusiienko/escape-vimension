using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthSlider;

    [Header("Bar Colors")]
    public Color healthBarColor = Color.red;
    public Color healthBarBackgroundColor = Color.gray;

    public void Awake()
    {
        if (healthSlider == null) healthSlider = GetComponent<Slider>();
    }

    public void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        if (healthSlider == null) return;
        float fillValue = currentHealth / maxHealth;

        healthSlider.value = fillValue;
    }
}
