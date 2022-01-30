using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 4f;
    public GameObject deathSound;
    public GameObject deathFlash;
    public UnityEvent onDeath;

    Slider healthSlider;
    float maxHealth;

    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        maxHealth = health;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(float damage)
    {
        SetHealth(health - damage);
        if (IsDead())
        {
            Die();
        }
    }

    public void Heal(int extraHealth)
    {
        SetHealth(health + extraHealth);
    }

    bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    void SetHealth(float newHealth) 
    {
        health = newHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthSlider.value = health;
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
