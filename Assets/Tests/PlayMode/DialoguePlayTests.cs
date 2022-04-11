using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class DialoguePlayTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_WithPlayer");
    }

    [UnityTest]
    public IEnumerator TypeSentence_FromNothing_SentenceIsTypedOverTime()
    {
        Assert.AreEqual("New Text", DialogueManager.instance.dialogueText.text);

        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = new Dialogue();
        dialogueTrigger.dialogue.name = "DUKE";
        dialogueTrigger.dialogue.sentences = new string[] { "HELLO", "NICE TO SEE YOU" };
        DialogueManager.instance.StartDialogue(dialogueTrigger.dialogue, dialogueTrigger);

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual("HELLO", DialogueManager.instance.dialogueText.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator StartDialogue_NoneNPC_VariablesAreSet()
    {
        Assert.AreEqual(true, DialogueManager.instance.dialogueTrigger == null);
        Assert.AreEqual(true, DialogueManager.instance.nameText.text == "New Text");
        Assert.AreEqual(0, DialogueManager.instance.sentences.Count);


        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = new Dialogue();
        dialogueTrigger.dialogue.name = "DUKE";
        dialogueTrigger.dialogue.sentences = new string[] {"HELLO", "NICE TO SEE YOU" };
        DialogueManager.instance.StartDialogue(dialogueTrigger.dialogue, dialogueTrigger);

        Assert.AreEqual(true, DialogueManager.instance.dialogueTrigger != null);
        Assert.AreEqual(true, DialogueManager.instance.nameText.text == "DUKE");
        Assert.AreEqual(1, DialogueManager.instance.sentences.Count);

        yield return null;
    }

    [UnityTest]
    public IEnumerator EndDialogue()
    {
        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = new Dialogue();
        dialogueTrigger.dialogue.name = "DUKE";
        dialogueTrigger.dialogue.sentences = new string[] { "HELLO", "NICE TO SEE YOU" };
        DialogueManager.instance.StartDialogue(dialogueTrigger.dialogue, dialogueTrigger);
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual("HELLO", DialogueManager.instance.dialogueText.text);
        DialogueManager.instance.DisplayNextSentence();
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual("NICE TO SEE YOU", DialogueManager.instance.dialogueText.text);
        DialogueManager.instance.DisplayNextSentence();
        Assert.AreEqual(false, DialogueManager.instance.animator.GetBool("isOpen"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator PopUp()
    {        
        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = new Dialogue();
        dialogueTrigger.dialogue.name = "DUKE";
        dialogueTrigger.dialogue.sentences = new string[] { "THIS IS THE POPUP TEST" };
        dialogueTrigger.popUpTime = 5.0f;
        Assert.AreEqual("New Text", DialogueManager.instance.dialogueText.text);
        Assert.AreEqual(false, DialogueManager.instance.animator.GetBool("isOpen"));
        dialogueTrigger.StartPopUp();
        yield return new WaitForSeconds(dialogueTrigger.popUpTime / 2);
        Assert.AreEqual("THIS IS THE POPUP TEST", DialogueManager.instance.dialogueText.text);
        Assert.AreEqual(true, DialogueManager.instance.animator.GetBool("isOpen"));
        yield return new WaitForSeconds(dialogueTrigger.popUpTime / 2);
        Assert.AreEqual(false, DialogueManager.instance.animator.GetBool("isOpen"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator StopAllDialogue()
    {
        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = new Dialogue();
        dialogueTrigger.dialogue.name = "DUKE";
        dialogueTrigger.dialogue.sentences = new string[] { "THIS IS THE POPUP TEST" };
        Assert.AreEqual(false, DialogueManager.instance.animator.GetBool("isOpen"));
        dialogueTrigger.StartPopUp();
        Assert.AreEqual(true, DialogueManager.instance.animator.GetBool("isOpen"));
        DialogueTrigger.StopAllDialogue();
        Assert.AreEqual(false, DialogueManager.instance.animator.GetBool("isOpen"));
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
