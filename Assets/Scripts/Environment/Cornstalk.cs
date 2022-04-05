using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornstalk : MonoBehaviour
{
    public GameObject leafRustleSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Instantiate(leafRustleSound, transform.position, Quaternion.identity);
        }
    }
}
