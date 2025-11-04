using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private Slider volumeSlider;
    
    private void OnEnable()
    {
        // Initialize when the menu becomes active
        InitializeSlider();
    }
    
    private void InitializeSlider()
    {
        // Find the slider if not assigned
        if (volumeSlider == null)
        {
            volumeSlider = GetComponentInChildren<Slider>();
        }
        
        // Make sure we have a slider
        if (volumeSlider == null)
        {
            Debug.LogWarning("OptionsMenu: Volume slider not found!");
            return;
        }
        
        // Remove listener first to avoid duplicate subscriptions
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        
        // Subscribe to slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        
        // Set initial slider value
        float initialVolume = 1.0f;
        
        if (AudioManager.Instance != null)
        {
            // Use AudioManager's master volume
            initialVolume = AudioManager.Instance.GetMasterVolume();
        }
        else
        {
            // Use current AudioListener volume or default to 1.0
            initialVolume = AudioListener.volume > 0 ? AudioListener.volume : 1.0f;
        }
        
        // Set the slider value (this won't trigger the event if we set it before adding listener)
        volumeSlider.value = Mathf.Clamp01(initialVolume);
    }
    
    private void OnVolumeChanged(float value)
    {
        // Clamp value to valid range
        value = Mathf.Clamp01(value);
        
        // Update AudioManager master volume (affects all audio sources)
        if (AudioManager.Instance != null)
        {
            // Set master volume which will update AudioListener and all managed audio
            AudioManager.Instance.SetMasterVolume(value);
        }
        else
        {
            // Fallback: Update AudioListener directly if AudioManager doesn't exist
            AudioListener.volume = value;
        }
    }
    
    private void OnDisable()
    {
        // Unsubscribe when menu is disabled to prevent memory leaks
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from slider events to prevent memory leaks
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}

