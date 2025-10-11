using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI levelText; // Shows current level
    public TextMeshProUGUI expText; // Shows exp progress (e.g., "150/200")
    public Image expBarFill; // The experience bar fill
    public Image expBarBackground; // The experience bar background
    
    [Header("Display Settings")]
    public string levelLabel = "Level: ";
    public string expLabel = "EXP: ";
    
    [Header("Bar Colors")]
    public Color expBarColor = Color.blue;
    public Color expBarBackgroundColor = Color.gray;
    public Color expBarGlowColor = Color.cyan;
    
    [Header("Animation Settings")]
    public bool animateBarFill = true;
    public float animationSpeed = 2f;
    
    [Header("Level Up Effects")]
    public Color levelUpFlashColor = Color.yellow;
    public float flashDuration = 0.5f;
    
    private LevelSystem levelSystem;
    private Coroutine flashCoroutine;
    private Coroutine barAnimationCoroutine;
    private float targetFillAmount = 0f;
    
    void Start()
    {
        // Initialize display
        UpdateDisplay();
    }
    
    /// <summary>
    /// Sets the level system reference and subscribes to events
    /// </summary>
    /// <param name="levelSys">The level system to connect to</param>
    public void SetLevelSystem(LevelSystem levelSys)
    {
        levelSystem = levelSys;
        
        // Subscribe to level system events
        if (levelSystem != null)
        {
            levelSystem.OnLevelUp += OnLevelUp;
            levelSystem.OnExpGained += OnExpGained;
            levelSystem.OnStrengthGained += OnStrengthGained;
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        if (levelSystem != null)
        {
            levelSystem.OnLevelUp -= OnLevelUp;
            levelSystem.OnExpGained -= OnExpGained;
            levelSystem.OnStrengthGained -= OnStrengthGained;
        }
    }
    
    /// <summary>
    /// Updates the experience display
    /// </summary>
    public void UpdateDisplay()
    {
        if (levelSystem == null) return;
        
        // Update level text
        if (levelText != null)
        {
            levelText.text = $"{levelLabel}{levelSystem.GetCurrentLevel()}";
        }
        
        // Update exp text
        if (expText != null)
        {
            expText.text = $"{expLabel}{levelSystem.GetCurrentExp()}/{levelSystem.GetExpToNextLevel()}";
        }
        
        // Update exp bar
        UpdateExpBar();
    }
    
    /// <summary>
    /// Updates the experience bar fill with smooth animation
    /// </summary>
    private void UpdateExpBar()
    {
        if (expBarFill == null) return;
        
        float progress = levelSystem.GetExpProgress();
        targetFillAmount = progress;
        
        // Set bar color
        expBarFill.color = expBarColor;
        
        // Set background color
        if (expBarBackground != null)
        {
            expBarBackground.color = expBarBackgroundColor;
        }
        
        // Animate the bar fill
        if (animateBarFill && barAnimationCoroutine == null)
        {
            barAnimationCoroutine = StartCoroutine(AnimateBarFill());
        }
        else if (!animateBarFill)
        {
            expBarFill.fillAmount = targetFillAmount;
        }
    }
    
    /// <summary>
    /// Animates the bar fill smoothly
    /// </summary>
    private System.Collections.IEnumerator AnimateBarFill()
    {
        float startFill = expBarFill.fillAmount;
        float elapsed = 0f;
        
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            float t = Mathf.SmoothStep(0f, 1f, elapsed);
            
            expBarFill.fillAmount = Mathf.Lerp(startFill, targetFillAmount, t);
            
            yield return null;
        }
        
        expBarFill.fillAmount = targetFillAmount;
        barAnimationCoroutine = null;
    }
    
    /// <summary>
    /// Called when player levels up
    /// </summary>
    /// <param name="newLevel">The new level</param>
    private void OnLevelUp(int newLevel)
    {
        Debug.Log($"ExpUI: Player reached level {newLevel}!");
        
        // Flash effect on level up
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(LevelUpFlash());
        
        // Update display
        UpdateDisplay();
    }
    
    /// <summary>
    /// Called when player gains experience
    /// </summary>
    /// <param name="currentExp">Current experience</param>
    /// <param name="expToNext">Experience needed for next level</param>
    private void OnExpGained(int currentExp, int expToNext)
    {
        UpdateDisplay();
    }
    
    /// <summary>
    /// Called when player gains strength
    /// </summary>
    /// <param name="newStrength">New total strength</param>
    private void OnStrengthGained(int newStrength)
    {
        Debug.Log($"ExpUI: Player strength is now {newStrength}!");
    }
    
    /// <summary>
    /// Creates a flash effect when leveling up
    /// </summary>
    private System.Collections.IEnumerator LevelUpFlash()
    {
        if (levelText == null) yield break;
        
        Color originalColor = levelText.color;
        
        // Flash to level up color
        levelText.color = levelUpFlashColor;
        yield return new WaitForSeconds(flashDuration);
        
        // Return to original color
        levelText.color = originalColor;
    }
}