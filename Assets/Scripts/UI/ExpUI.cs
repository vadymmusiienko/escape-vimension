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
    
    
    private LevelSystem levelSystem;
    
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
        
        // Check if player is at max level
        bool isAtMaxLevel = levelSystem.IsAtMaxLevel();
        
        // Update level text
        if (levelText != null)
        {
            levelText.text = $"{levelLabel}{levelSystem.GetCurrentLevel()}";
        }
        
        // Hide/show exp text based on max level
        if (expText != null)
        {
            if (isAtMaxLevel)
            {
                expText.gameObject.SetActive(false);
            }
            else
            {
                expText.gameObject.SetActive(true);
                expText.text = $"{expLabel}{levelSystem.GetCurrentExp()}/{levelSystem.GetExpToNextLevel()}";
            }
        }
        
        // Hide/show exp bar based on max level
        if (expBarFill != null)
        {
            expBarFill.gameObject.SetActive(!isAtMaxLevel);
        }
        
        if (expBarBackground != null)
        {
            expBarBackground.gameObject.SetActive(!isAtMaxLevel);
        }
        
        // Update exp bar only if not at max level
        if (!isAtMaxLevel)
        {
            UpdateExpBar();
        }
    }
    
    /// <summary>
    /// Updates the experience bar fill
    /// </summary>
    private void UpdateExpBar()
    {
        if (expBarFill == null) return;
        
        float progress = levelSystem.GetExpProgress();
        expBarFill.fillAmount = progress;
        
        // Set bar color
        expBarFill.color = expBarColor;
        
        // Set background color
        if (expBarBackground != null)
        {
            expBarBackground.color = expBarBackgroundColor;
        }
    }
    
    
    /// <summary>
    /// Called when player levels up
    /// </summary>
    /// <param name="newLevel">The new level</param>
    private void OnLevelUp(int newLevel)
    {
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
    }
    
}