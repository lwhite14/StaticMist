using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterAttack : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Monster monster;
    public float damage = 4f;
    public float strikingDistanceBuffer = 0.2f;
    public float attackCooldown = 0.5f;
    float attackCooldownCounter;
    public UnityEvent<float> attack;

    void Start()
    {
        attackCooldownCounter = attackCooldown;
    }

    void Update()
    {
        CheckStrikingDistance();
    }

    void CheckStrikingDistance() 
    {
        if (monster.CanSeePlayer()) 
        {
            float dist = navMeshAgent.remainingDistance;
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= (navMeshAgent.stoppingDistance + strikingDistanceBuffer))
                {
                    if (monster.GetIsChasing())
                    {
                        if (attackCooldownCounter < 0) 
                        {
                            Attack();
                            attackCooldownCounter = attackCooldown;
                        }
                    }          
                }
            }
        }

        if (!(attackCooldownCounter < 0))
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void Attack() 
    {
        attack.Invoke(damage);
    }

}
