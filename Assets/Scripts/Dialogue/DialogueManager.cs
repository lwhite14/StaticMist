using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public float textScrollTime = 0.1f;

    DialogueTrigger dialogueTrigger;
    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger usedDialogueTrigger) 
    {
        dialogueTrigger = usedDialogueTrigger;

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

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

    IEnumerator TypeSentence(string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textScrollTime);
        }
    }

    void EndDialogue() 
    {
        animator.SetBool("isOpen", false);
        dialogueTrigger.SetIsTriggered(false);
    }

}
