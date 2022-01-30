using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterAttack : MonoBehaviour
{
    public GameObject stabSound;
    public float damage = 4f;
    public float strikingDistanceBuffer = 0.2f;
    public float attackCooldown = 0.5f;
    float attackCooldownCounter;
    public UnityEvent attack;
    NavMeshAgent navMeshAgent;
    MonsterPathfinding monster;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monster = GetComponent<MonsterPathfinding>();
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
        attack.Invoke();
        FindObjectOfType<Health>().TakeDamage(damage);
    }

    public void AttackSound() 
    {
        Instantiate(stabSound, transform.position, Quaternion.identity);
    }

}
