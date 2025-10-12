using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    [Header("UI References")]
    public Image cooldownFill;
    public Text cooldownText;
    
    private PlayerDashInput dashInput;
    
    void Start()
    {
        // Find the player's dash input component
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            dashInput = player.GetComponent<PlayerDashInput>();
        }
        
        // Hide UI if no dash input found
        if (dashInput == null)
        {
            gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (dashInput == null) return;
        
        float cooldownRemaining = dashInput.GetCooldownRemaining();
        float cooldownTotal = dashInput.dashCooldown;
        
        // Update fill amount
        if (cooldownFill != null)
        {
            cooldownFill.fillAmount = 1f - (cooldownRemaining / cooldownTotal);
        }
        
        // Update text
        if (cooldownText != null)
        {
            if (cooldownRemaining > 0)
            {
                cooldownText.text = $"Dash: {cooldownRemaining:F1}s";
            }
            else
            {
                cooldownText.text = "Dash: Ready";
            }
        }
    }
}
