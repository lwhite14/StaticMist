using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health = 4f;
    public GameObject deathSound;
    public GameObject deathFlash;
    public UnityEvent onDeath;

    public void TakeDamage(float damage) 
    {
        health -= damage;
        if (IsDead()) 
        {
            Die();
        }
    }

    bool IsDead() 
    {
        if (health <= 0) 
        {
            return true;
        }
        return false;
    }

    void Die() 
    {
        onDeath.Invoke();
    }

    public void DeathSound() 
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);
    }

    public void PlayDeathFlash() 
    {
        Instantiate(deathFlash, GameObject.Find("FlashTarget").transform);
    }
}
