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
    [Test]
    public void PlayerMovement_Instantiation_ScriptExists()
    {
        GameObject player = new GameObject();
        PlayerMovement playerMovement = player.AddComponent<PlayerMovement>();

        Assert.NotNull(playerMovement);
    }

    [Test]
    public void SetXSetZ_NewValues() 
    {
        GameObject player = new GameObject();
        PlayerMovement playerMovement = player.AddComponent<PlayerMovement>();

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
        GameObject player = new GameObject();
        PlayerMovement playerMovement = player.AddComponent<PlayerMovement>();

        Assert.AreEqual(true, playerMovement.CheckNotMoving());
        playerMovement.SetX(2);
        playerMovement.SetZ(2);
        Assert.AreEqual(false, playerMovement.CheckNotMoving());
    }
}
