using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip tenseMusic;
    public AudioClip chaseMusic;
    public AudioClip goalMusic;
    AudioSource audioSource;

    int chaseCounter = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tenseMusic;
        audioSource.Play();
    }

    public void SwitchToTense() 
    {
        chaseCounter--;
        if (chaseCounter == 0)
        {
            audioSource.clip = tenseMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SwitchToChase()
    {
        chaseCounter++;
        if (chaseCounter == 1)
        {
            audioSource.clip = chaseMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SwitchToGoal() 
    {
        audioSource.clip = goalMusic;
        audioSource.loop = false;
        audioSource.Play();
    }

}
