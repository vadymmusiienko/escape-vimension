using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100; // Experience needed for next level
    public int expPerLevel = 50; // Additional exp needed per level (scaling)
    public int maxLevel = 6; // Maximum level cap
    
    [Header("Level Benefits")]
    public int strengthPerLevel = 10; // Strength gained per level
    
    [Header("Level-based Growth Settings")]
    [Tooltip("Size increase for level 1")]
    public float level1SizeIncrease = 0.0f;
    [Tooltip("Size increase for level 2")]
    public float level2SizeIncrease = 0.2f;
    [Tooltip("Size increase for level 3")]
    public float level3SizeIncrease = 0.35f;
    [Tooltip("Size increase for level 4")]
    public float level4SizeIncrease = 0.4f;
    [Tooltip("Size increase for level 5")]
    public float level5SizeIncrease = 0.4f;
    [Tooltip("Size increase for level 6")]
    public float level6SizeIncrease = 2f;
    
    [Header("Speed Settings")]
    [Tooltip("Base movement speed")]
    public float baseSpeed = 0.8f;
    [Tooltip("Speed increase for level 1")]
    public float level1SpeedIncrease = 0.0f;
    [Tooltip("Speed increase for level 2")]
    public float level2SpeedIncrease = 1.3f;
    [Tooltip("Speed increase for level 3")]
    public float level3SpeedIncrease = 2.5f;
    [Tooltip("Speed increase for level 4")]
    public float level4SpeedIncrease = 2f;
    [Tooltip("Speed increase for level 5")]
    public float level5SpeedIncrease = 2f;
    [Tooltip("Speed increase for level 6")]
    public float level6SpeedIncrease = 1.5f;
    
    [Header("Current Stats")]
    public int totalStrength; // Total strength from all levels
    public float currentSize = 1f; // Current size multiplier
    public float currentSpeed; // Current movement speed
    
    // Events for UI updates
    public System.Action<int> OnLevelUp;
    public System.Action<int, int> OnExpGained; // currentExp, expToNextLevel
    public System.Action<int> OnStrengthGained; // new total strength
    
    void Start()
    {
        // Initialize starting values
        totalStrength = 10; // Base strength
        currentSpeed = baseSpeed; // Initialize speed to base speed
        
        // Don't reset currentSize - preserve Inspector values
        // Only apply the size if it's not already applied
        UpdatePlayerSize();
    }
    
    /// <summary>
    /// Adds experience points and handles leveling up
    /// </summary>
    /// <param name="expAmount">Amount of experience to add</param>
    public void AddExperience(int expAmount)
    {
        // Don't add experience if already at max level
        if (currentLevel >= maxLevel)
        {
            return;
        }
        
        currentExp += expAmount;
        
        // Notify UI of exp gain
        OnExpGained?.Invoke(currentExp, expToNextLevel);
        
        // Check if we can level up (but not beyond max level)
        while (currentExp >= expToNextLevel && currentLevel < maxLevel)
        {
            LevelUp();
        }
    }
    
    /// <summary>
    /// Handles leveling up the player
    /// </summary>
    private void LevelUp()
    {
        currentLevel++;
        currentExp -= expToNextLevel;
        
        // Increase exp requirement for next level
        expToNextLevel += expPerLevel;
        
        // Gain strength from leveling
        totalStrength += strengthPerLevel;
        
        // Increase size based on level-specific growth
        float sizeIncrease = GetSizeIncreaseForLevel(currentLevel);
        currentSize += sizeIncrease;
        UpdatePlayerSize();
        
        // Increase speed based on level-specific growth
        float speedIncrease = GetSpeedIncreaseForLevel(currentLevel);
        currentSpeed += speedIncrease;
        
        
        // Notify listeners
        OnLevelUp?.Invoke(currentLevel);
        OnStrengthGained?.Invoke(totalStrength);
        OnExpGained?.Invoke(currentExp, expToNextLevel);
    }
    
    /// <summary>
    /// Calculates the size increase for a given level
    /// </summary>
    private float GetSizeIncreaseForLevel(int level)
    {
        // Ensure level is within valid range
        if (level < 1 || level > maxLevel)
        {
            return 0f;
        }
        
        switch (level)
        {
            case 1:
                return level1SizeIncrease;
            case 2:
                return level2SizeIncrease;
            case 3:
                return level3SizeIncrease;
            case 4:
                return level4SizeIncrease;
            case 5:
                return level5SizeIncrease;
            case 6:
                return level6SizeIncrease;
            default:
                return 0f; // No size increase beyond max level
        }
    }
    
    /// <summary>
    /// Calculates the speed increase for a given level
    /// </summary>
    private float GetSpeedIncreaseForLevel(int level)
    {
        // Ensure level is within valid range
        if (level < 1 || level > maxLevel)
        {
            return 0f;
        }
        
        switch (level)
        {
            case 1:
                return level1SpeedIncrease;
            case 2:
                return level2SpeedIncrease;
            case 3:
                return level3SpeedIncrease;
            case 4:
                return level4SpeedIncrease;
            case 5:
                return level5SpeedIncrease;
            case 6:
                return level6SpeedIncrease;
            default:
                return 0f; // No speed increase beyond max level
        }
    }
    
    /// <summary>
    /// Gets the current speed of the player
    /// </summary>
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    
    /// <summary>
    /// Updates the player's visual size
    /// </summary>
    private void UpdatePlayerSize()
    {
        transform.localScale = Vector3.one * currentSize;
    }
    
    /// <summary>
    /// Gets the current level
    /// </summary>
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    
    /// <summary>
    /// Gets the current experience
    /// </summary>
    public int GetCurrentExp()
    {
        return currentExp;
    }
    
    /// <summary>
    /// Gets the experience needed for next level
    /// </summary>
    public int GetExpToNextLevel()
    {
        return expToNextLevel;
    }
    
    /// <summary>
    /// Gets the total strength from all levels
    /// </summary>
    public int GetTotalStrength()
    {
        return totalStrength;
    }
    
    /// <summary>
    /// Gets the current size multiplier
    /// </summary>
    public float GetCurrentSize()
    {
        return currentSize;
    }
    
    /// <summary>
    /// Gets the experience progress as a percentage (0-1)
    /// </summary>
    public float GetExpProgress()
    {
        return (float)currentExp / expToNextLevel;
    }
    
    /// <summary>
    /// Checks if the player is at maximum level
    /// </summary>
    public bool IsAtMaxLevel()
    {
        return currentLevel >= maxLevel;
    }
    
    /// <summary>
    /// Gets the maximum level
    /// </summary>
    public int GetMaxLevel()
    {
        return maxLevel;
    }
}