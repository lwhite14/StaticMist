using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    public AudioClip tenseMusic;
    public AudioClip chaseMusic;
    public AudioClip goalMusic;
    public AudioClip menuMusic;
    AudioSource audioSource;

    bool tenseIsPlaying = false;
    bool chaseIsPlaying = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameManager.instance.level == 0)
        {
            SwitchToMenu();
        }
        else 
        {
            SwitchToTense();
        }
    }

    public void SwitchToTense() 
    {
        MonsterPathfinding[] monsters = FindObjectsOfType<MonsterPathfinding>();
        bool stillBeingChased = false;
        foreach (MonsterPathfinding monster in monsters) 
        {
            if (monster.GetIsChasing()) 
            {
                stillBeingChased = true;
            }
        }

        if (!tenseIsPlaying && !stillBeingChased)
        {
            chaseIsPlaying = false;
            tenseIsPlaying = true;

            audioSource.clip = tenseMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
     
    }

    public void SwitchToChase()
    {
        if (!chaseIsPlaying)
        {
            chaseIsPlaying = true;
            tenseIsPlaying = false;

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

    public void SwitchToMenu() 
    {
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Pause() 
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.Pause();
        }
    }

    public void Unpause() 
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.UnPause();
        }
    }

}
