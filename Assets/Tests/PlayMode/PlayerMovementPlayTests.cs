using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class PlayerMovementPlayTests
{
    [SetUp]
    public void SetUp() 
    {
        
    }

    [UnityTest]
    public IEnumerator Player_NewScene_PlayerExists()
    {
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator MoveForward_MovementKeys_ChangeXPosition()
    {

        yield return null;
    }

    public IEnumerator MoveLaterally_MovementKeys_ChangeZPosition()
    {

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckGrounded_OnFloor_ReturnsTrue()
    {

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckGrounded_Jumped_ReturnsFalse()
    {

        yield return null;
    }

    [UnityTest]
    public IEnumerator Jump_SpaceKey_ChangeYPosition()
    {

        yield return null;
    }


}
