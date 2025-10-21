using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour, IDialogueTrigger
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private bool deleteAfterDialogue = true;
    [SerializeField] private bool triggerOnlyOnce = false;
    
    private bool hasTriggered = false;
    private TrapTeleportTrigger trapTeleportTrigger;
    
    private void Awake()
    {
        // Get the trap teleport trigger component on this GameObject
        trapTeleportTrigger = GetComponent<TrapTeleportTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if we should only trigger once
            if (triggerOnlyOnce && hasTriggered)
            {
                return;
            }
            
            DialogueManager.instance.DialogueStart(dialogueStrings, this);
            hasTriggered = true;
        }
    }
    
    public void DeleteTrigger()
    {
        // Notify trap teleport trigger that dialogue is finished
        if (trapTeleportTrigger != null)
        {
            trapTeleportTrigger.OnDialogueFinished();
        }
        
        if (deleteAfterDialogue)
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class dialogueString { 
    public string text;
    public bool isEnd;

    [Header("Triggered Event")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
