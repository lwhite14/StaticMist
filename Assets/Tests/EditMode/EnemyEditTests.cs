using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EnemyEditTests
{
    [Test]
    public void MonsterContructerAndGet()
    {
        Monster monster = new Monster("Testing");
        Assert.That(monster.GetName() != "Tetsing");
        Assert.That(monster.GetName() == "Testing");
    }

    [Test]
    public void MonsterSetName()
    {
        Monster monster = new Monster();
        Assert.That(monster.GetName() == null);
        monster.SetName("Testing");
        Assert.That(monster.GetName() != "Tetsing");
        Assert.That(monster.GetName() == "Testing");
    }

    [Test]
    public void MonsterHealthTakeDamage()
    {
        MonsterHealth monsterHealth = new GameObject().AddComponent<MonsterHealth>();
        monsterHealth.health = 10.0f;
        Assert.That(monsterHealth.health == 10.0f);
        monsterHealth.TakeDamage(1.0f);
        Assert.That(monsterHealth.health == 9.0f);
        monsterHealth.TakeDamage(1.0f);
        monsterHealth.TakeDamage(2.0f);
        Assert.That(monsterHealth.health == 6.0f);
        monsterHealth.TakeDamage(10.0f);
        Assert.That(monsterHealth.health == 0);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]