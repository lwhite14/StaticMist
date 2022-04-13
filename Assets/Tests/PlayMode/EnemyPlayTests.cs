using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EnemyPlayTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_WithEnemy");
    }

    [UnityTest]
    public IEnumerator MonsterAnimationAndSoundPlaysSounds()
    {
        GameObject zombie = GameObject.FindWithTag("Monster");
        Assert.That(GameObject.FindWithTag("Sound") == null);
        zombie.GetComponent<MonsterAnimationAndSound>().MonsterSpottedStab();
        yield return new WaitForEndOfFrame();
        Assert.That(GameObject.FindWithTag("Sound") != null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MonsterAttack_TriggersWhenInRange()
    {
        GameObject player = GameObject.Find("Player");
        GameObject zombie = GameObject.FindWithTag("Monster");
        MonsterAttack monsterAttack = zombie.GetComponent<MonsterAttack>();
        player.transform.position = new Vector3(-6.25f, 0.88f, -9.75f);
        Assert.That(monsterAttack.standingAttack == false);
        yield return new WaitForSeconds(4.0f);
        Assert.That(monsterAttack.standingAttack == true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerSpottedTriggered()
    {
        GameObject player = GameObject.Find("Player");
        GameObject zombie = GameObject.FindWithTag("Monster");
        PlayerSpotted playerSpotted = zombie.GetComponent<PlayerSpotted>();
        Assert.That(playerSpotted.hasSpotted == false);
        player.transform.position = new Vector3(-6.25f, 0.88f, -9.75f);
        player.transform.RotateAround(player.transform.position, Vector3.up, 90);
        yield return new WaitForSeconds(1.0f);
        Assert.That(playerSpotted.hasSpotted == true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MonsterPathfindingCanSeePlayer()
    {
        GameObject player = GameObject.Find("Player");
        GameObject zombie = GameObject.FindWithTag("Monster");
        MonsterPathfinding monsterPathfinding = zombie.GetComponent<MonsterPathfinding>();
        monsterPathfinding.stopChance = 0.0f;
        yield return new WaitForEndOfFrame();
        Assert.AreEqual(false, monsterPathfinding.CanSeePlayer());
        Assert.AreEqual(false, monsterPathfinding.CanSeePlayerClose());
        yield return new WaitForSeconds(5.0f);
        Assert.AreEqual(true, monsterPathfinding.CanSeePlayer());
        yield return null;
    }

    [UnityTest]
    public IEnumerator MonsterPathfindingChasesPlayer()
    {
        GameObject player = GameObject.Find("Player");
        GameObject zombie = GameObject.FindWithTag("Monster");
        MonsterPathfinding monsterPathfinding = zombie.GetComponent<MonsterPathfinding>();
        monsterPathfinding.stopChance = 0.0f;
        yield return new WaitForEndOfFrame();
        Assert.AreEqual(false, monsterPathfinding.CanSeePlayer());
        Assert.AreEqual(false, monsterPathfinding.CanSeePlayerClose());
        Assert.AreEqual(false, monsterPathfinding.GetIsChasing());
        yield return new WaitForSeconds(7.0f);
        Assert.AreEqual(true, monsterPathfinding.CanSeePlayer());
        Assert.AreEqual(true, monsterPathfinding.GetIsChasing());
        player.transform.position = new Vector3(-14.0f, 0.88f, 14.0f);
        Assert.AreEqual(false, monsterPathfinding.CanSeePlayer());
        Assert.AreEqual(true, monsterPathfinding.GetIsChasing());
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
