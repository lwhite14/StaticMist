using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject deathSound;
    public GameObject deathFlash;
    public GameObject hurtFlash;
    public UnityEvent onDeath;
    public float health = 4.0f;
    public float maxHealth = 4.0f; 

    Slider healthSlider;
    [HideInInspector] public bool isDead = false;

    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(float damage, string monsterType)
    {
        SetHealth(health - damage);
        if (IsDead())
        {
            Die(monsterType);
        }
        else 
        {
            PlayHurtFlash();
        }
    }

    public void Heal(float extraHealth)
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

    public void SetHealth(float newHealth) 
    {
        health = newHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthSlider.value = health;
    }

    public void InitSetHealth(float newHealth) 
    {
        health = newHealth;
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        else 
        { 
            GameObject.Find("HealthSlider").GetComponent<Slider>().value = health;
        }
    }

    void Die(string monsterType) 
    {
        // At this point the player has died, and so I send an event to Unity Analytics.
        SendDataToAnalytics(monsterType);
        onDeath.Invoke();
        GameManager.instance.OnDeath();
        isDead = true;
    }

    public void DeathSound() 
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);
    }

    public void PlayDeathFlash() 
    {
        Instantiate(deathFlash, GameObject.Find("FlashTarget").transform);
    }

    public void PlayHurtFlash() 
    {
        Instantiate(hurtFlash, GameObject.Find("FlashTarget").transform);
    }

    public float GetHealth() 
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    void SendDataToAnalytics(string monsterType) 
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Monster", monsterType },
                { "userLevel", FindObjectOfType<GameManager>().level }
            };
            Events.CustomData("Died", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'Died' with: Monster = " + monsterType + ", and userLevel = " + FindObjectOfType<GameManager>().level.ToString());
        }
    }
}
