using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationAndSound : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;

    public AudioClip passiveSound;
    public AudioClip chaseSound;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = passiveSound;
        RandomisePassiveSound();
    }

    public void SetSpeed(float newSpeed) 
    {
        anim.SetFloat("speed", newSpeed);
    }

    public void PlayAttack() 
    {
        anim.Play("Attack");
    }

    void RandomisePassiveSound() 
    {
        System.Random random = new System.Random();
        double randomDouble = random.NextDouble() * (double)audioSource.clip.length;
        audioSource.time = (float)randomDouble;
        audioSource.Play();
    }

    public void SwitchToPassive() 
    {
        audioSource.clip = passiveSound;
        audioSource.Play();
    }

    public void SwitchToChase()
    {
        audioSource.clip = chaseSound;
        audioSource.Play();
    }
}
