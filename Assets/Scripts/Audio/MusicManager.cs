using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip tenseMusic;
    public AudioClip chaseMusic;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SwitchToTense();
    }

    public void SwitchToTense() 
    {
        audioSource.clip = tenseMusic;
        audioSource.Play(); 
    }

    public void SwitchToChase()
    {
        audioSource.clip = chaseMusic;
        audioSource.Play();
    }

}
