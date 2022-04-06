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
        SwitchToPassiveRandom();
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

    void SwitchToPassiveRandom() 
    {
        float randomTime = Random.Range(0.0f, 5.0f);
        audioSource.clip = passiveSound;
        audioSource.time = randomTime;
        audioSource.Play();
    }

    public void SwitchToPassive() 
    {
        audioSource.time = 0.0f;
        audioSource.clip = passiveSound;
        audioSource.Play();
    }

    public void SwitchToChase()
    {
        audioSource.time = 0.0f;
        audioSource.clip = chaseSound;
        audioSource.Play();
    }

    public void SwitchToDeath() 
    {
        audioSource.time = 0.0f;
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
