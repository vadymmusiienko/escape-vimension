using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource playerAudioSource;
    
    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float musicVolume = 0.5f;
    [SerializeField] private bool playOnStart = true;
    
    [Header("Sound Effects")]
    [SerializeField] private float sfxVolume = 0.7f;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private float levelUpVolume = 0.8f;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private float damageVolume = 0.7f;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Initialize audio sources if not assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        
        if (playerAudioSource == null)
        {
            playerAudioSource = gameObject.AddComponent<AudioSource>();
        }
        
        SetupAudioSources();
    }
    
    private void Start()
    {
        if (playOnStart && backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }
    
    private void SetupAudioSources()
    {
        // Configure music source for background music
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.playOnAwake = false;
        musicSource.priority = 0; // High priority for music
        
        // Configure SFX source for sound effects
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;
        sfxSource.playOnAwake = false;
        sfxSource.priority = 128; // Lower priority for SFX
        
        // Configure player audio source for movement sounds
        playerAudioSource.loop = true;
        playerAudioSource.volume = 0.5f;
        playerAudioSource.playOnAwake = false;
        playerAudioSource.priority = 64; // Medium priority for player sounds
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else
        {
        }
    }
    
    public void StopBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    
    public void PauseBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }
    
    public void ResumeBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void PlayLevelUpSound()
    {
        if (levelUpSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(levelUpSound, levelUpVolume);
        }
    }
    
    public void PlayDamageSound()
    {
        if (damageSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(damageSound, damageVolume);
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
    }
    
    public float GetMusicVolume()
    {
        return musicVolume;
    }
    
    public float GetSFXVolume()
    {
        return sfxVolume;
    }
    
    public bool IsMusicPlaying()
    {
        return musicSource != null && musicSource.isPlaying;
    }
    
    // Player audio methods
    public void PlayPlayerAudio(AudioClip clip, float volume = 0.5f)
    {
        if (clip != null && playerAudioSource != null)
        {
            playerAudioSource.clip = clip;
            playerAudioSource.volume = volume;
            playerAudioSource.Play();
        }
    }
    
    public void StopPlayerAudio()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.Stop();
        }
    }
    
    public void SetPlayerAudioVolume(float volume)
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
    
    public AudioSource GetPlayerAudioSource()
    {
        return playerAudioSource;
    }
}
