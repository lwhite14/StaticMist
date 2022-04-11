using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class DialogueEditTests
{
    [Test]
    public void DialogueClass_SetName()
    {
        Dialogue dialogue = new Dialogue();
        Assert.AreEqual(null, dialogue.name);
        dialogue.name = "DUKE";
        Assert.AreEqual("DUKE", dialogue.name);
    }

    [Test]
    public void DialogueClass_SetSentences()
    {
        Dialogue dialogue = new Dialogue();
        Assert.AreEqual(null, dialogue.sentences);
        dialogue.sentences = new string[]
        { 
            "HELLO",
            "NICE TO SEE YOU"    
        };
        Assert.AreEqual(new string[]{"HELLO", "NICE TO SEE YOU"}, dialogue.sentences);
    }

    [Test]
    public void DialogueTrigger_SetIsTriggered()
    {
        GameObject obj = new GameObject();
        DialogueTrigger dialogueTrigger = obj.AddComponent<DialogueTrigger>();
        Assert.AreEqual(false, dialogueTrigger.GetIsTriggered());
        dialogueTrigger.SetIsTriggered(true);
        Assert.AreEqual(true, dialogueTrigger.GetIsTriggered());
    }

}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]