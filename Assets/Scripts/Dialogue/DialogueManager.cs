using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private float typingSpeed = 0.05f;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;
    [Range(0f, 1f)] [SerializeField] private float typingVolume = 0.5f;

    private List<dialogueString> dialogueList;
    private DialogueTrigger currentTrigger;

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private bool isDialogueActive = false;
    private bool isTypingSoundPlaying = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogueParent.SetActive(false);
    }

    /*
    void Update()
    {
        if (dialogueParent.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueList[currentDialogueIndex].text;
                isTyping = false;
            }
        }
    }*/

    public void DialogueStart(List<dialogueString> textToPrint, DialogueTrigger trigger = null)
    {
        if (dialogueParent.activeSelf) return;
        
        dialogueParent.SetActive(true);
        dialogueList = textToPrint;
        currentTrigger = trigger;
        currentDialogueIndex = 0;
        isTyping = false;
        isDialogueActive = true;

        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke();

            Coroutine typingCoroutine = StartCoroutine(typeText(line.text));

            while (isTyping)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = line.text;
                    StopTypingSound();
                    isTyping = false;
                }
                yield return null;
            }

            line.endDialogueEvent?.Invoke();

            if (!line.isEnd)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                yield return null;
            }

            currentDialogueIndex++;
        }

        DialogueStop();
    }

    private IEnumerator typeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        
        // Start typing sound
        StartTypingSound();
        
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        // Stop typing sound
        StopTypingSound();
        isTyping = false;
    }

    private void DialogueStop()
    {
        dialogueText.text = ""; 
        dialogueParent.SetActive(false);
        isDialogueActive = false;
        
        // Stop typing sound when dialogue ends
        StopTypingSound();
        
        // Delete trigger if it should be deleted after dialogue
        if (currentTrigger != null)
        {
            currentTrigger.DeleteTrigger();
            currentTrigger = null;
        }
    }
    
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
    
    private void StartTypingSound()
    {
        if (audioSource != null && typingSound != null && !isTypingSoundPlaying)
        {
            audioSource.clip = typingSound;
            audioSource.loop = true;
            audioSource.volume = typingVolume;
            audioSource.Play();
            isTypingSoundPlaying = true;
        }
    }
    
    private void StopTypingSound()
    {
        if (audioSource != null && isTypingSoundPlaying)
        {
            audioSource.Stop();
            isTypingSoundPlaying = false;
        }
    }
}
