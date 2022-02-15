using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationAndSound : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;

    [Header("Monster Sound Clips")]
    public AudioClip passiveSound;
    public AudioClip chaseSound;
    public AudioClip deathSound;

    [Header("Musical Stabs")]
    public GameObject monsterSpottedStab;
    public GameObject playerSpottedStab;

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

    public void PlayDeath() 
    {
        anim.Play("Death");
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

    public void SwitchToDeath() 
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void MonsterSpottedStab()
    {
        Instantiate(monsterSpottedStab, transform.position, Quaternion.identity);
    }

    public void PlayerSpottedStab() 
    {
        Instantiate(playerSpottedStab, transform.position, Quaternion.identity);
    }
}
