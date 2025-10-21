using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider slider;

    public void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }

    public void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        if (slider == null) return;
        float fillValue = currentHealth / maxHealth;

        slider.value = fillValue;
    }
}
