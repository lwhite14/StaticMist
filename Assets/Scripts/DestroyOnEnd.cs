using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnd : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!source.isPlaying) 
        {
            Die();
        }
    }

    void Die() 
    {
        Destroy(gameObject);
    }
}
