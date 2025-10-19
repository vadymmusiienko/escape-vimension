using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip pickupClip;
    
    [Header("Audio Settings")]
    [SerializeField] private float runningVolume = 0.6f;
    [SerializeField] private float runningPitch = 0.8f; // Higher pitch = faster sound
    [SerializeField] private float pickupVolume = 0.8f;
    
    // References
    private Player player;
    private PlayerMovement movement;
    private PlayerInteraction interaction;
    private AudioSource runningAudioSource;
    private AudioSource pickupAudioSource;
    
    // State tracking
    private bool wasMoving = false;
    
    private void Awake()
    {
        player = GetComponent<Player>();
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<PlayerInteraction>();
        
        // Create dedicated audio sources for movement sounds
        SetupAudioSources();
    }
    
    private void Start()
    {
        // Load audio clips if not assigned
        LoadDefaultAudioClips();
    }
    
    private void Update()
    {
        if (player == null || movement == null) return;
        
        HandleMovementAudio();
    }
    
    private void SetupAudioSources()
    {
        // Create running audio source
        runningAudioSource = gameObject.AddComponent<AudioSource>();
        runningAudioSource.clip = runningClip;
        runningAudioSource.loop = true;
        runningAudioSource.volume = 0f;
        runningAudioSource.pitch = runningPitch;
        runningAudioSource.playOnAwake = false;
        runningAudioSource.priority = 64; // Medium priority for movement sounds
        
        // Create pickup audio source
        pickupAudioSource = gameObject.AddComponent<AudioSource>();
        pickupAudioSource.clip = pickupClip;
        pickupAudioSource.loop = false;
        pickupAudioSource.volume = pickupVolume;
        pickupAudioSource.playOnAwake = false;
        pickupAudioSource.priority = 32; // High priority for pickup sounds
    }
    
    private void LoadDefaultAudioClips()
    {
        // Try to load Running.mp3 from the Audio folder
        if (runningClip == null)
        {
            runningClip = Resources.Load<AudioClip>("Audio/Running");
            if (runningClip == null)
            {
                Debug.LogWarning("Running audio clip not found! Please assign it manually.");
            }
        }
        
        // Try to load PickUp.mp3 from the Audio folder
        if (pickupClip == null)
        {
            pickupClip = Resources.Load<AudioClip>("Audio/PickUp");
            if (pickupClip == null)
            {
                Debug.LogWarning("PickUp audio clip not found! Please assign it manually.");
            }
        }
    }
    
    private void HandleMovementAudio()
    {
        bool isCurrentlyMoving = movement.Speed > 0.01f;
        
        // Start audio when moving starts
        if (isCurrentlyMoving && !wasMoving)
        {
            StartRunningAudio();
        }
        // Stop audio immediately when movement stops
        else if (!isCurrentlyMoving && wasMoving)
        {
            StopRunningAudio();
        }
        
        wasMoving = isCurrentlyMoving;
    }
    
    private void StartRunningAudio()
    {
        if (runningAudioSource != null && runningClip != null)
        {
            runningAudioSource.volume = runningVolume;
            runningAudioSource.pitch = runningPitch;
            runningAudioSource.Play();
            Debug.Log("Started running audio");
        }
    }
    
    private void StopRunningAudio()
    {
        if (runningAudioSource != null)
        {
            runningAudioSource.Stop();
            Debug.Log("Stopped running audio");
        }
    }
    
    // Public methods for external control
    public void SetRunningVolume(float volume)
    {
        runningVolume = Mathf.Clamp01(volume);
    }
    
    public void SetRunningPitch(float pitch)
    {
        runningPitch = Mathf.Clamp(pitch, 0.1f, 3f); // Reasonable pitch range
        if (runningAudioSource != null)
        {
            runningAudioSource.pitch = runningPitch;
        }
    }
    
    public void PlayPickupSound()
    {
        if (pickupAudioSource != null && pickupClip != null)
        {
            pickupAudioSource.Play();
            Debug.Log("Played pickup sound");
        }
    }
    
    public void SetPickupVolume(float volume)
    {
        pickupVolume = Mathf.Clamp01(volume);
        if (pickupAudioSource != null)
        {
            pickupAudioSource.volume = pickupVolume;
        }
    }
}
