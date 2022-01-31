using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip tenseMusic;
    public AudioClip chaseMusic;
    public AudioClip goalMusic;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SwitchToTense();
    }

    public void SwitchToTense() 
    {
        audioSource.clip = tenseMusic;
        audioSource.loop = true;
        audioSource.Play(); 
    }

    public void SwitchToChase()
    {
        audioSource.clip = chaseMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SwitchToGoal() 
    {
        audioSource.clip = goalMusic;
        audioSource.loop = false;
        audioSource.Play();
    }

}
