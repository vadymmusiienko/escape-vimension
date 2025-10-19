using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100; // Experience needed for next level
    public int expPerLevel = 50; // Additional exp needed per level (scaling)
    
    [Header("Level Benefits")]
    public int strengthPerLevel = 10; // Strength gained per level
    
    [Header("Level-based Growth Settings")]
    [Tooltip("Size increase for level 1")]
    public float level1SizeIncrease = 0.3f;
    [Tooltip("Size increase for level 2")]
    public float level2SizeIncrease = 0.2f;
    [Tooltip("Size increase for level 3")]
    public float level3SizeIncrease = 0.1f;
    [Tooltip("Size increase for all levels 4 and above")]
    public float generalLevelSizeIncrease = 0.1f;
    
    [Header("Current Stats")]
    public int totalStrength; // Total strength from all levels
    public float currentSize = 1f; // Current size multiplier
    
    // Events for UI updates
    public System.Action<int> OnLevelUp;
    public System.Action<int, int> OnExpGained; // currentExp, expToNextLevel
    public System.Action<int> OnStrengthGained; // new total strength
    
    void Start()
    {
        // Initialize starting values
        totalStrength = 10; // Base strength
        
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
        currentExp += expAmount;
        Debug.Log($"Gained {expAmount} EXP! Total: {currentExp}/{expToNextLevel}");
        
        // Notify UI of exp gain
        OnExpGained?.Invoke(currentExp, expToNextLevel);
        
        // Check if we can level up
        while (currentExp >= expToNextLevel)
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
        
        Debug.Log($"LEVEL UP! Now level {currentLevel}! +{strengthPerLevel} strength! Size increased by {sizeIncrease:F2}!");
        
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
        switch (level)
        {
            case 1:
                return level1SizeIncrease;
            case 2:
                return level2SizeIncrease;
            case 3:
                return level3SizeIncrease;
            default:
                return generalLevelSizeIncrease;
        }
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
}