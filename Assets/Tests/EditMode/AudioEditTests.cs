using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class AudioEditTests
{
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        MusicManager.instance = obj.AddComponent<MusicManager>();
        MusicManager.instance.audioSource = obj.GetComponent<AudioSource>();
        MusicManager.instance.tenseMusic = AudioClip.Create("Tense", 44100 * 2, 1, 44100, true);
        MusicManager.instance.chaseMusic = AudioClip.Create("Chase", 44100 * 2, 1, 44100, true);
        MusicManager.instance.goalMusic = AudioClip.Create("Goal", 44100 * 2, 1, 44100, true);

    }

    [Test]
    public void PlayTense_Silent_PlaysTenseMusic()
    {
        Assert.AreEqual(false, MusicManager.instance.tenseIsPlaying);
        MusicManager.instance.SwitchToTense();
        Assert.AreEqual(true, MusicManager.instance.tenseIsPlaying);
        Assert.AreEqual("Tense", MusicManager.instance.audioSource.clip.name);
    }

    [Test]
    public void PlayChase_Silent_PlaysChaseMusic()
    {
        Assert.AreEqual(false, MusicManager.instance.chaseIsPlaying);
        MusicManager.instance.SwitchToChase();
        Assert.AreEqual(true, MusicManager.instance.chaseIsPlaying);
        Assert.AreEqual("Chase", MusicManager.instance.audioSource.clip.name);
    }

    [Test]
    public void PlayGoal_Silent_PlaysGoalMusic()
    {
        Assert.AreEqual(false, MusicManager.instance.chaseIsPlaying);
        Assert.AreEqual(false, MusicManager.instance.tenseIsPlaying);
        MusicManager.instance.SwitchToGoal();
        Assert.AreEqual("Goal", MusicManager.instance.audioSource.clip.name);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]