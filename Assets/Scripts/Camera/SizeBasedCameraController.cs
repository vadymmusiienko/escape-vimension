using UnityEngine;

public class SizeBasedCameraController : MonoBehaviour
{
    [Header("References")]
    public LevelSystem levelSystem; // Assign manually in Inspector
    public MonoBehaviour positionComposer; // Assign the Position Composer component manually
    
    [Header("Camera Distance Settings")]
    public float baseCameraDistance = 10f; // Base distance for size 1.0 (matches your Position Composer)
    public float minDistance = 3f; // Minimum distance (for very small players)
    public float maxDistance = 20f; // Maximum distance (for very large players)
    
    [Header("Level-based Camera Distance Changes")]
    [Tooltip("Camera distance change for level 1")]
    public float level1DistanceChange = 0f;
    [Tooltip("Camera distance change for level 2")]
    public float level2DistanceChange = 3f;
    [Tooltip("Camera distance change for level 3")]
    public float level3DistanceChange = 2f;
    [Tooltip("Camera distance change for level 4")]
    public float level4DistanceChange = 2f;
    [Tooltip("Camera distance change for level 5")]
    public float level5DistanceChange = 2f;
    [Tooltip("Camera distance change for level 6")]
    public float level6DistanceChange = 2f;
    
    [Header("Smoothing")]
    public float distanceChangeSpeed = 2f; // How fast the camera adjusts
    
    // Current values
    private float targetDistance;
    private float currentDistance;
    
    void Start()
    {
        // Initialize with base distance
        currentDistance = baseCameraDistance;
        targetDistance = baseCameraDistance;
        
    }
    
    void Update()
    {
        if (levelSystem != null)
        {
            // Calculate target distance based on player size (original logic)
            float playerSize = levelSystem.GetCurrentSize();
            float sizeBasedDistance = baseCameraDistance * playerSize;
            
            // Add level-based distance changes on top of size-based distance
            int currentLevel = levelSystem.GetCurrentLevel();
            float levelDistanceChange = GetDistanceChangeForLevel(currentLevel);
            
            // Calculate final target distance: size-based + level-based changes
            targetDistance = sizeBasedDistance + levelDistanceChange;
            
            // Clamp the distance to min/max values
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            
            // Smoothly adjust the camera distance
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, distanceChangeSpeed * Time.deltaTime);
            
            // Apply the distance to the camera
            UpdateCameraDistance();
        }
        else
        {
            // LevelSystem is null
        }
    }
    
    /// <summary>
    /// Calculates the cumulative distance change for a given level
    /// </summary>
    private float GetDistanceChangeForLevel(int level)
    {
        float totalDistanceChange = 0f;
        
        // Add distance changes for each level individually
        if (level >= 1) totalDistanceChange += level1DistanceChange;
        if (level >= 2) totalDistanceChange += level2DistanceChange;
        if (level >= 3) totalDistanceChange += level3DistanceChange;
        if (level >= 4) totalDistanceChange += level4DistanceChange;
        if (level >= 5) totalDistanceChange += level5DistanceChange;
        if (level >= 6) totalDistanceChange += level6DistanceChange;
        
        return totalDistanceChange;
    }
    
    /// <summary>
    /// Updates the camera distance by modifying the Position Composer's Camera Distance
    /// </summary>
    private void UpdateCameraDistance()
    {
        if (positionComposer != null)
        {
            try
            {
                // Debug output removed since it's working
                
                // Try to find the CameraDistance field (not property)
                var cameraDistanceField = positionComposer.GetType().GetField("CameraDistance", 
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (cameraDistanceField == null)
                {
                    cameraDistanceField = positionComposer.GetType().GetField("cameraDistance", 
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                }
                
                if (cameraDistanceField == null)
                {
                    cameraDistanceField = positionComposer.GetType().GetField("m_CameraDistance", 
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                }
                
                if (cameraDistanceField != null)
                {
                    cameraDistanceField.SetValue(positionComposer, currentDistance);
                    
                }
                else
                {
                    // Could not find CameraDistance field on Position Composer
                }
            }
            catch (System.Exception)
            {
                // Error updating camera distance
            }
        }
        else
        {
            // Position Composer reference is null
        }
    }
    
    /// <summary>
    /// Gets the current camera distance
    /// </summary>
    public float GetCurrentDistance()
    {
        return currentDistance;
    }
    
    /// <summary>
    /// Gets the target camera distance
    /// </summary>
    public float GetTargetDistance()
    {
        return targetDistance;
    }
    
    /// <summary>
    /// Manually set the base camera distance
    /// </summary>
    public void SetBaseDistance(float newBaseDistance)
    {
        baseCameraDistance = newBaseDistance;
    }
    
    /// <summary>
    /// Gets the current player level
    /// </summary>
    public int GetCurrentLevel()
    {
        return levelSystem != null ? levelSystem.GetCurrentLevel() : 0;
    }
}