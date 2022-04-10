using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class PlayerMovementEditTests
{
    GameObject player;
    PlayerMovement playerMovement;
    PlayerSprinting playerSprinting;
    PlayerCrouching playerCrouching;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject();
        playerMovement = player.AddComponent<PlayerMovement>();
        playerSprinting = player.AddComponent<PlayerSprinting>();
        playerCrouching = player.AddComponent<PlayerCrouching>();
    }

    [Test]
    public void PlayerMovement_Instantiation_ScriptExists()
    {
        Assert.NotNull(playerMovement);
    }

    [Test]
    public void SetXSetZ_NewValues()
    {
        Assert.AreEqual(0, playerMovement.GetX());
        Assert.AreEqual(0, playerMovement.GetZ());
        playerMovement.SetX(5);
        playerMovement.SetZ(17.12f);
        Assert.AreEqual(5, playerMovement.GetX());
        Assert.AreEqual(17.12f, playerMovement.GetZ());
    }

    [Test]
    public void CheckNotMoving_StillAndMoving()
    {
        Assert.AreEqual(true, playerMovement.CheckNotMoving());
        playerMovement.SetX(2);
        playerMovement.SetZ(2);
        Assert.AreEqual(false, playerMovement.CheckNotMoving());
    }

    [Test]
    public void ChangeSpeed()
    {
        float preSpeed = playerMovement.GetSpeed();
        playerMovement.ChangeSpeed(10.0f);
        float postSpeed = playerMovement.GetSpeed();

        Assert.AreEqual(10.0f, postSpeed);
        Assert.AreNotEqual(preSpeed, postSpeed);
    }

    [Test]
    public void SprintInput_FalseAndTrue() 
    {
        playerSprinting.SprintInput(true);
        Assert.AreEqual(playerSprinting.GetInputPressed(), true); 
        playerSprinting.SprintInput(false);
        Assert.AreEqual(playerSprinting.GetInputPressed(), false);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]