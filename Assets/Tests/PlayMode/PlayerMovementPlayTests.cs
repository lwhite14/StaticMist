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
        Assert.IsNotNull(playerMovement);
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

}
