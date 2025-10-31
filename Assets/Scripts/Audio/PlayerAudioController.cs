using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip hitEnemyClip;
    [SerializeField] private AudioClip attackSwingClip;
    
    [Header("Audio Settings")]
    [SerializeField] private float runningVolume = 0.6f;
    [SerializeField] private float runningPitch = 0.8f; // Higher pitch = faster sound
    [SerializeField] private float pickupVolume = 0.8f;
    [SerializeField] private float dropVolume = 0.7f;
    [SerializeField] private float dashVolume = 0.7f;
    [SerializeField] private float hitEnemyVolume = 0.8f;
    [SerializeField] private float attackSwingVolume = 0.7f;
    
    [Header("Drop Sound Timing")]
    [SerializeField] private float dropSoundDelay = 0.5f; // Delay before drop sound plays
    
    // References
    private Player player;
    private PlayerMovement movement;
    private PlayerInteraction interaction;
    private AudioSource runningAudioSource;
    private AudioSource pickupAudioSource;
    private AudioSource dropAudioSource;
    private AudioSource dashAudioSource;
    private AudioSource hitEnemyAudioSource;
    private AudioSource attackSwingAudioSource;
    
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
        // Play drop sound with delay when player spawns
        Invoke(nameof(PlayDropSound), dropSoundDelay);
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
        
        // Create drop audio source
        dropAudioSource = gameObject.AddComponent<AudioSource>();
        dropAudioSource.clip = dropClip;
        dropAudioSource.loop = false;
        dropAudioSource.volume = dropVolume;
        dropAudioSource.playOnAwake = false;
        dropAudioSource.priority = 16; // Highest priority for drop sound
        
        // Create dash audio source
        dashAudioSource = gameObject.AddComponent<AudioSource>();
        dashAudioSource.clip = dashClip;
        dashAudioSource.loop = false; // Will be controlled manually for duration
        dashAudioSource.volume = dashVolume;
        dashAudioSource.playOnAwake = false;
        dashAudioSource.priority = 32; // High priority for dash sound
        
        // Create hit enemy audio source
        hitEnemyAudioSource = gameObject.AddComponent<AudioSource>();
        hitEnemyAudioSource.clip = hitEnemyClip;
        hitEnemyAudioSource.loop = false;
        hitEnemyAudioSource.volume = hitEnemyVolume;
        hitEnemyAudioSource.playOnAwake = false;
        hitEnemyAudioSource.priority = 16; // Highest priority for hit sounds
        
        // Create attack swing audio source
        attackSwingAudioSource = gameObject.AddComponent<AudioSource>();
        attackSwingAudioSource.clip = attackSwingClip;
        attackSwingAudioSource.loop = false;
        attackSwingAudioSource.volume = attackSwingVolume;
        attackSwingAudioSource.playOnAwake = false;
        attackSwingAudioSource.priority = 32; // High priority for attack sounds
    }
    
    
    private void HandleMovementAudio()
    {
        // Don't play audio if movement is locked
        if (movement.IsMovementLocked())
        {
            if (wasMoving)
            {
                StopRunningAudio();
                wasMoving = false;
            }
            return;
        }
        
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
        }
    }
    
    private void StopRunningAudio()
    {
        if (runningAudioSource != null)
        {
            runningAudioSource.Stop();
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
    
    public void PlayDropSound()
    {
        if (dropAudioSource != null && dropClip != null)
        {
            dropAudioSource.Play();
        }
    }
    
    public void SetDropVolume(float volume)
    {
        dropVolume = Mathf.Clamp01(volume);
        if (dropAudioSource != null)
        {
            dropAudioSource.volume = dropVolume;
        }
    }
    
    public void SetDropSoundDelay(float delay)
    {
        dropSoundDelay = Mathf.Max(0f, delay);
    }
    
    // Dash audio methods
    public void PlayDashSound(float duration)
    {
        if (dashAudioSource != null && dashClip != null)
        {
            // Stop any currently playing dash sound
            dashAudioSource.Stop();
            
            // Set the clip and play it
            dashAudioSource.clip = dashClip;
            dashAudioSource.volume = dashVolume;
            dashAudioSource.Play();
            
            // Stretch the audio to match the dash duration
            if (dashClip.length > 0)
            {
                dashAudioSource.pitch = dashClip.length / duration;
            }
            
        }
    }
    
    public void StopDashSound()
    {
        if (dashAudioSource != null && dashAudioSource.isPlaying)
        {
            dashAudioSource.Stop();
            dashAudioSource.pitch = 1f; // Reset pitch
        }
    }
    
    public void SetDashVolume(float volume)
    {
        dashVolume = Mathf.Clamp01(volume);
        if (dashAudioSource != null)
        {
            dashAudioSource.volume = dashVolume;
        }
    }
    
    // Hit enemy audio methods
    public void PlayHitEnemySound()
    {
        if (hitEnemyAudioSource != null && hitEnemyClip != null)
        {
            hitEnemyAudioSource.Play();
        }
    }
    
    public void SetHitEnemyVolume(float volume)
    {
        hitEnemyVolume = Mathf.Clamp01(volume);
        if (hitEnemyAudioSource != null)
        {
            hitEnemyAudioSource.volume = hitEnemyVolume;
        }
    }
    
    // Attack swing audio methods
    public void PlayAttackSwingSound()
    {
        if (attackSwingAudioSource != null && attackSwingClip != null)
        {
            attackSwingAudioSource.Play();
        }
    }
    
    public void SetAttackSwingVolume(float volume)
    {
        attackSwingVolume = Mathf.Clamp01(volume);
        if (attackSwingAudioSource != null)
        {
            attackSwingAudioSource.volume = attackSwingVolume;
        }
    }
}
