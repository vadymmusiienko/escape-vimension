using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class QuitCommandDialogueTrigger : MonoBehaviour, IDialogueTrigger
{
    [Header("Dialogue Settings")]
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    
    [Header("Scene Settings")]
    [SerializeField] private string endSceneName = "EndScene";
    
    [Header("Command Settings")]
    [SerializeField] private bool triggerOnlyOnce = true;
    [SerializeField] private float commandTimeout = 2f; // Time in seconds to wait for 'q' after colon
    
    private bool hasTriggered = false;
    private bool waitingForQ = false;
    private float colonPressTime = 0f;
    private string commandBuffer = ""; // Buffer to accumulate command input
    
    void Update()
    {
        // Don't process if already triggered and should only trigger once
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }
        
        // Don't process if dialogue is currently active
        if (DialogueManager.instance != null && DialogueManager.instance.IsDialogueActive())
        {
            return;
        }
        
        HandleQuitCommand();
    }
    
    private void HandleQuitCommand()
    {
        // Get current frame's input string (this captures typed characters)
        string currentInput = Input.inputString;
        
        // Also check for semicolon key with shift (which produces colon)
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Semicolon))
        {
            currentInput = ":";
        }
        
        // Accumulate input into buffer
        if (!string.IsNullOrEmpty(currentInput))
        {
            commandBuffer += currentInput.ToLower();
        }
        
        // Also check direct key presses (fallback method)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (waitingForQ)
            {
                // We were waiting for 'q' after colon
                waitingForQ = false;
                if (CanTrigger())
                {
                    TriggerDialogue();
                }
            }
        }
        
        // Keep buffer limited to last few characters to catch ":q"
        if (commandBuffer.Length > 10)
        {
            commandBuffer = commandBuffer.Substring(commandBuffer.Length - 10);
        }
        
        // Check if buffer contains ":q" sequence
        if (commandBuffer.Contains(":q"))
        {
            if (CanTrigger())
            {
                commandBuffer = "";
                waitingForQ = false;
                TriggerDialogue();
            }
        }
        // Also set waiting state if we see a colon
        else if (commandBuffer.EndsWith(":"))
        {
            waitingForQ = true;
            colonPressTime = Time.time;
        }
        
        // Reset waiting state if timeout
        if (waitingForQ && Time.time - colonPressTime > commandTimeout)
        {
            waitingForQ = false;
            // Clear buffer if we've been waiting too long
            if (commandBuffer.EndsWith(":"))
            {
                commandBuffer = "";
            }
        }
    }
    
    private bool CanTrigger()
    {
        return !triggerOnlyOnce || !hasTriggered;
    }
    
    public void TriggerDialogue()
    {
        // Check if we should trigger (only once if specified)
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }
        
        // Trigger dialogue
        if (DialogueManager.instance != null && dialogueStrings.Count > 0)
        {
            DialogueManager.instance.DialogueStart(dialogueStrings, this);
            hasTriggered = true;
        }
    }
    
    // This method will be called by DialogueManager when dialogue ends
    public void DeleteTrigger()
    {
        // Load the end scene after dialogue finishes
        LoadEndScene();
        
        // Don't destroy the GameObject, just destroy this component
        // (since it might be attached to Player or GameManager)
        Destroy(this);
    }
    
    private void LoadEndScene()
    {
        if (!string.IsNullOrEmpty(endSceneName))
        {
            SceneManager.LoadScene(endSceneName);
        }
        else
        {
            Debug.LogError("QuitCommandDialogueTrigger: End scene name is not set!");
        }
    }
}

