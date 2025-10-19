using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Player References")]
    public Player player;
    
    [Header("UI References")]
    public ExpUI expUI;
    public ClipboardUI clipboardUI;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        // Find player if not assigned
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        
        // Find UI components if not assigned
        if (expUI == null)
        {
            expUI = FindFirstObjectByType<ExpUI>();
        }
        
        if (clipboardUI == null)
        {
            clipboardUI = FindFirstObjectByType<ClipboardUI>();
        }
        
        // Input is handled directly in individual scripts
    }
}
