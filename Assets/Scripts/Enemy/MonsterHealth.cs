using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage) 
    {
        health -= damage;
        if (health <= 0) 
        {
            health = 0;
            if (Application.isPlaying)
            {
                Die();
            }
        }
    }

    void Die() 
    {
        GetComponent<MonsterPathfinding>().StopAllCoroutines();
        GetComponent<MonsterPathfinding>().SetIsChasing(false);

        GetComponent<MonsterAnimationAndSound>().PlayDeath();
        GetComponent<MonsterAnimationAndSound>().SwitchToDeath();
        MusicManager.instance.SwitchToTense();
        NPC.PlayerEscaped();

        GetComponent<MonsterAttack>().enabled = false;
        GetComponent<MonsterPathfinding>().enabled = false;
        GetComponent<PlayerSpotted>().enabled = false;

        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
