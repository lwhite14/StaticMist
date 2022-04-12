using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class InteractionPlayTests
{
    Interact interact;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_Interactables");
    }

    [UnityTest]
    public IEnumerator CastInteractRays_InteractableInRange_TriggersInteraction()
    {
        InteractableObject interactableObject = GameObject.FindObjectOfType<InteractableObject>();
        bool preInteracted = interactableObject.hasInteracted;
        Assert.AreEqual(false, preInteracted);
        GameObject.FindObjectOfType<Interact>().InteractInput();
        bool postInteracted = interactableObject.hasInteracted;
        Assert.AreEqual(true, postInteracted);
        yield return null;
    }

    [UnityTest]
    public IEnumerator UpdateRays_NoInteractable_SetsVariables()
    {
        Animator crossHairAnim = GameObject.Find("Crosshair").GetComponent<Animator>();
        InteractableObject interactableObject = GameObject.FindObjectOfType<InteractableObject>();
        interactableObject.Interact();
        Assert.AreEqual(true, interactableObject.hasInteracted);
        Assert.AreEqual(true, crossHairAnim.GetBool("isOpen"));
        interactableObject.gameObject.transform.position = new Vector3(0, 100, 0);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Assert.AreEqual(false, crossHairAnim.GetBool("isOpen"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator Interactable_Interacted_PlaysSound()
    {
        InteractableMedKit interactableMedKit = GameObject.FindObjectOfType<InteractableMedKit>();
        interactableMedKit.Interact();
        yield return new WaitForSeconds(0.5f);
        AudioSource source = GameObject.Find("PickUp(Clone)").GetComponent<AudioSource>();
        Assert.AreEqual(true, source.isPlaying);      
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
