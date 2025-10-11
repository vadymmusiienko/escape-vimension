using UnityEngine;

public class PlayerLeveling : MonoBehaviour
{
    [Header("Level System")]
    public LevelSystem levelSystem;
    public ExpUI expUI;
    
    void Start()
    {
        InitializeLevelSystem();
    }
    
    private void InitializeLevelSystem()
    {
        if (levelSystem == null)
        {
            levelSystem = GetComponent<LevelSystem>();
            if (levelSystem == null)
            {
                levelSystem = gameObject.AddComponent<LevelSystem>();
            }
        }
        
        if (expUI != null && levelSystem != null)
        {
            expUI.SetLevelSystem(levelSystem);
        }
    }
    
    public void AddExperience(int expAmount)
    {
        if (levelSystem != null)
        {
            levelSystem.AddExperience(expAmount);
        }
    }
    
    public int GetCurrentStrength()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetTotalStrength();
        }
        return 10; // Default fallback
    }
    
    public int GetCurrentLevel()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetCurrentLevel();
        }
        return 1; // Default fallback
    }
    
    public int GetCurrentExp()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetCurrentExp();
        }
        return 0; // Default fallback
    }
    
    public int GetExpToNextLevel()
    {
        if (levelSystem != null)
        {
            return levelSystem.GetExpToNextLevel();
        }
        return 100; // Default fallback
    }
}
