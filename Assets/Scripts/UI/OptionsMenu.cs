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
        // Try to get current volume from AudioManager first
        float initialVolume = 1.0f;
        
        if (AudioManager.Instance != null)
        {
            // Use AudioManager's current volume settings
            // Average of music and SFX volume, or use AudioListener volume
            initialVolume = AudioListener.volume;
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
        
        // Update AudioListener master volume (affects all audio)
        AudioListener.volume = value;
        
        // Also update AudioManager volumes if it exists
        if (AudioManager.Instance != null)
        {
            // Update both music and SFX volumes proportionally
            AudioManager.Instance.SetMusicVolume(value);
            AudioManager.Instance.SetSFXVolume(value);
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

