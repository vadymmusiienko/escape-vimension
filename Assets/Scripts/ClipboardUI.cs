using UnityEngine;
using UnityEngine.UI;

public class ClipboardUI : MonoBehaviour
{
    [Header("UI References")]
    public Text clipboardText;
    public Image clipboardIcon;
    public GameObject clipboardPanel;
    
    [Header("Visual Settings")]
    public Color emptyColor = Color.gray;
    public Color filledColor = Color.blue;
    
    private Player player;
    
    void Start()
    {
        // Find the player
        player = FindFirstObjectByType<Player>();
        
        // Hide clipboard panel initially
        if (clipboardPanel != null)
            clipboardPanel.SetActive(false);
    }
    
    void Update()
    {
        if (player == null) return;
        
        // Check if player has something in clipboard
        bool hasClipboardItem = player.clipboardItem != null;
        
        // Show/hide clipboard panel
        if (clipboardPanel != null)
            clipboardPanel.SetActive(hasClipboardItem);
        
        // Update clipboard text
        if (clipboardText != null)
        {
            if (hasClipboardItem)
            {
                clipboardText.text = $"Clipboard: {player.clipboardItem.itemName}";
                clipboardText.color = filledColor;
            }
            else
            {
                clipboardText.text = "Clipboard: Empty";
                clipboardText.color = emptyColor;
            }
        }
        
        // Update clipboard icon
        if (clipboardIcon != null)
        {
            clipboardIcon.color = hasClipboardItem ? filledColor : emptyColor;
        }
    }
}
