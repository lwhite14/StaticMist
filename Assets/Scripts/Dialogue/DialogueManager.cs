﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;

    public float textScrollTime = 0.1f;

    DialogueTrigger dialogueTrigger;
    Queue<string> sentences;
    Text nameText;
    Text dialogueText;
    Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
        nameText = GameObject.Find("Name").GetComponent<Text>();
        dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
        animator = GameObject.Find("DialogueBox").GetComponent<Animator>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger usedDialogueTrigger) 
    {
        dialogueTrigger = usedDialogueTrigger;
        dialogueTrigger.SetIsTriggered(true);

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    // Queues every line in the given dialogue class and sets the NPCs name on the UI.

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    // Ends dialogue if no lines left, and runs a coroutine with the line as a parameter. 

    IEnumerator TypeSentence(string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textScrollTime);
        }
    }
    // Displays the sentence letter by letter, with the time between each letter given by a adjustable variable.

    public void EndDialogue() 
    {
        animator.SetBool("isOpen", false);
        if (dialogueTrigger != null)
        {
            dialogueTrigger.SetIsTriggered(false);
        }
    }
    // Ends the dialogue.

}
