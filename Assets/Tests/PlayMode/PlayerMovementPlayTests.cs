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
        SceneManager.LoadScene("PlayerMovementTest");
    }

    [UnityTest]
    public IEnumerator Player_NewScene_ObjectExists() 
    {
        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerMovement_NewScene_ScriptExists()
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        PlayerSprinting PlayerSprinting = GameObject.FindObjectOfType<PlayerSprinting>();
        PlayerCrouching playerCrouching = GameObject.FindObjectOfType<PlayerCrouching>();
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(PlayerSprinting);
        Assert.IsNotNull(playerCrouching);
        yield return null;
    }


    [UnityTest]
    public IEnumerator WarpToPosition_NewVector3_PlayerMoves()
    {
        Transform playerTransform = GameObject.Find("Player").transform;
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        Assert.Greater(playerTransform.position.y, 0);
        playerMovement.WarpToPosition(new Vector3(0, 10f, 0));
        Assert.AreEqual(new Vector3(0, 10f, 0), playerTransform.position);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Jump_JumpInput_PlayerJumps() 
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        float preY = playerMovement.gameObject.transform.position.y;
        playerMovement.JumpInput();
        yield return new WaitForSeconds(0.25f);
        float postY = playerMovement.gameObject.transform.position.y;
        Assert.AreNotEqual(preY, postY);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckGrounded_MidJump_ReturnFalse() 
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.JumpInput();
        bool isGrounded = playerMovement.CheckGrounded();
        Assert.AreEqual(true, isGrounded);
        yield return new WaitForSeconds(0.25f);
        isGrounded = playerMovement.CheckGrounded();
        Assert.AreEqual(false, isGrounded);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckMoving_MovingForward_ReturnFalse()
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        bool isMoving = playerMovement.CheckNotMoving();
        Assert.AreEqual(true, isMoving);

        float counter = 1.0f;
        while (counter > 0) 
        {
            counter -= Time.deltaTime;
            playerMovement.MovementSlideZ(1.0f);
            yield return null;
        }
        isMoving = playerMovement.CheckNotMoving();
        Assert.AreEqual(false, isMoving);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Sprinting_SprintForward_IncreasedSpeed() 
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        PlayerSprinting playerSprinting = GameObject.FindObjectOfType<PlayerSprinting>();
        float preSpeed = playerMovement.GetSpeed();
        float postSpeed = preSpeed;
        Assert.AreEqual(preSpeed, postSpeed);

        float counter = 1.0f;
        playerSprinting.SprintInput(true);
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            playerMovement.MovementSlideZ(1.0f);
            postSpeed = playerMovement.GetSpeed();
            yield return null;
        }

        Assert.AreNotEqual(preSpeed, postSpeed);
        yield return new WaitForSeconds(2.0f);
        postSpeed = playerMovement.GetSpeed();
        Assert.AreEqual(preSpeed, postSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SprintingFOV_SprintForward_FOVIncreasesThenDecreases() 
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        PlayerSprinting playerSprinting = GameObject.FindObjectOfType<PlayerSprinting>();
        float preFov = mainCamera.fieldOfView; 
        float postFov = preFov;
        Assert.AreEqual(preFov, postFov);

        float counter = 1.0f;
        playerSprinting.SprintInput(true);
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            playerMovement.MovementSlideZ(1.0f);
            postFov = mainCamera.fieldOfView;
            yield return null;
        }

        Assert.AreNotEqual(preFov, postFov);
        yield return new WaitForSeconds(2.0f);
        postFov = mainCamera.fieldOfView;
        Assert.AreEqual(preFov, postFov);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Crouching_NotMoving_ControllerHeighDecreases() 
    {
        PlayerCrouching playerCrouching = GameObject.FindObjectOfType<PlayerCrouching>();

        float preHeight = GameObject.FindObjectOfType<CharacterController>().height;
        float postHeight = preHeight;

        playerCrouching.CrouchInput();
        yield return new WaitForSeconds(1.0f);
        postHeight = GameObject.FindObjectOfType<CharacterController>().height;
        Assert.IsTrue(postHeight < preHeight);

        playerCrouching.CrouchInput();
        yield return new WaitForSeconds(1.0f);
        postHeight = GameObject.FindObjectOfType<CharacterController>().height;
        Assert.AreEqual(postHeight, preHeight);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Crouching_MovingForward_SpeedDecreases() 
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        PlayerCrouching playerCrouching = GameObject.FindObjectOfType<PlayerCrouching>();
        float preSpeed = playerMovement.GetSpeed();
        float postSpeed = preSpeed;

        Assert.AreEqual(preSpeed, postSpeed);

        playerCrouching.CrouchInput();
        yield return new WaitForSeconds(1.0f);

        postSpeed = playerMovement.GetSpeed();
        Assert.IsTrue(postSpeed < preSpeed);

        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
