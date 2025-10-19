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
        
        Debug.Log($"SizeBasedCameraController: Initialized with base distance {baseCameraDistance}");
    }
    
    void Update()
    {
        if (levelSystem != null)
        {
            // Calculate target distance based on player size
            // Smaller players = closer camera (smaller distance)
            // Larger players = further camera (larger distance)
            float playerSize = levelSystem.GetCurrentSize();
            targetDistance = baseCameraDistance * playerSize; // Direct relationship
            
            // Clamp the distance to min/max values
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            
            // Smoothly adjust the camera distance
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, distanceChangeSpeed * Time.deltaTime);
            
            // Apply the distance to the camera
            UpdateCameraDistance();
        }
        else
        {
            Debug.LogWarning("SizeBasedCameraController: LevelSystem is null!");
        }
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
                    
                    // Debug output
                    if (Time.frameCount % 60 == 0) // Log every 60 frames
                    {
                        Debug.Log($"Camera Distance: {currentDistance:F2}, Player size: {(levelSystem != null ? levelSystem.GetCurrentSize() : 0):F2}");
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find CameraDistance field on Position Composer");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error updating camera distance: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Position Composer reference is null!");
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
}