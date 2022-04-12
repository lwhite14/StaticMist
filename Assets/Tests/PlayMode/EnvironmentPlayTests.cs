using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EnvironmentPlayTests
{

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_Interactables");
    }

    [UnityTest]
    public IEnumerator Cornstalk_Colliding_MakeSound()
    {
        Cornstalk cornstalk = GameObject.FindObjectOfType<Cornstalk>();
        GameObject player = GameObject.Find("Player");

        Assert.That(GameObject.Find("LeafRustle(Clone)") == null);
        player.gameObject.transform.position = cornstalk.gameObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        Assert.That(GameObject.Find("LeafRustle(Clone)") != null);
        yield return null;
    }    
    
    [UnityTest]
    public IEnumerator Gate_NoKeyAndLocked_TriggerPopup()
    {
        Gate gate = GameObject.FindObjectOfType<Gate>();
        Animator dialogueAnim = GameObject.Find("DialogueBox").GetComponent<Animator>();
        gate.isLocked = true;
        Assert.AreEqual(false, dialogueAnim.GetBool("isOpen"));
        gate.Interact();
        Assert.AreEqual(true, dialogueAnim.GetBool("isOpen"));
        yield return null;
    }   
    
    [UnityTest]
    public IEnumerator LightFlicker()
    {
        LightFlicker lightFlicker = GameObject.FindObjectOfType<LightFlicker>();
        yield return new WaitForSeconds(0.25f);
        Assert.AreEqual(false, lightFlicker.pointLight.enabled);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(true, lightFlicker.pointLight.enabled);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(false, lightFlicker.pointLight.enabled);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Gate_Unlocked_ChangeOpenState()
    {
        Gate gate = GameObject.FindObjectOfType<Gate>();
        Animator gateAnim = gate.gameObject.GetComponentInChildren<Animator>();
        Assert.AreEqual(false, gateAnim.GetBool("isOpen"));
        gate.Interact();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(true, gateAnim.GetBool("isOpen"));
        gate.Interact();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(false, gateAnim.GetBool("isOpen"));
        gate.isLocked = true;
        gate.Interact();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(false, gateAnim.GetBool("isOpen"));
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
