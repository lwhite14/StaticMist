using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class AudioPlayTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_WithPlayer"); 
    }

    [UnityTest]
    public IEnumerator PlayTense_StartOfScene_MuiscPlays()
    {
        Assert.AreEqual(true, MusicManager.instance.tenseIsPlaying);
        Assert.AreEqual("tense2", MusicManager.instance.audioSource.clip.name);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PauseSound_PausedGame_AllSoundStop()
    {
        GameObject[] sounds = new GameObject[5];
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i] = new GameObject();
            AudioSource source = sounds[i].AddComponent<AudioSource>();
            source.loop = true;
            source.clip = AudioClip.Create("TestClip", 4500, 1, 4410, true);
            source.Play();
            Assert.AreEqual(true, source.isPlaying);
        }
        MusicManager.instance.Pause();
        foreach (GameObject soundObj in sounds)
        {
            AudioSource source = soundObj.GetComponent<AudioSource>();
            Assert.AreEqual(false, source.isPlaying);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator UnpauseSound_UnpausedGame_AllSoundResume()
    {
        GameObject[] sounds = new GameObject[5];
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i] = new GameObject();
            AudioSource source = sounds[i].AddComponent<AudioSource>();
            source.loop = true;
            source.clip = AudioClip.Create("TestClip", 4500, 1, 4410, true);
            Assert.AreEqual(false, source.isPlaying);
            source.Play();
        }
        MusicManager.instance.Pause();
        MusicManager.instance.Unpause();
        foreach (GameObject soundObj in sounds)
        {
            AudioSource source = soundObj.GetComponent<AudioSource>();
            Assert.AreEqual(true, source.isPlaying);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator DestroyOnEnd() 
    {
        GameObject soundObj = new GameObject();
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.clip = AudioClip.Create("TestClip", 2000, 1, 4410, true);
        soundObj.AddComponent<DestroyOnEndAudio>();
        Assert.That(soundObj != null);
        yield return new WaitForSeconds(0.5f);
        Assert.That(soundObj == null);
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
