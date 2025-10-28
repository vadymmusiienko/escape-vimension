using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [Header("UI References")]
    public Slider expSlider; // The experience bar slider
    
    [Header("Bar Colors")]
    public Color expBarColor = Color.blue;
    public Color expBarBackgroundColor = Color.gray;
    
    
    private LevelSystem levelSystem;
    
    void Start()
    {
        // Initialize display
        UpdateDisplay();
    }
    
    void Awake()
    {
        if (expSlider == null) expSlider = GetComponent<Slider>();
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
        
        // Hide/show exp slider based on max level
        if (expSlider != null)
        {
            expSlider.gameObject.SetActive(!isAtMaxLevel);
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
        if (expSlider == null) return;
        
        float progress = levelSystem.GetExpProgress();
        expSlider.value = progress;
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