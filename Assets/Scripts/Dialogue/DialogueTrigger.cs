using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private bool deleteAfterDialogue = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.DialogueStart(dialogueStrings, this);
        }
    }
    
    public void DeleteTrigger()
    {
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
