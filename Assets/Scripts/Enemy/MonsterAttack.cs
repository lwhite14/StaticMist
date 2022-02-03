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
    NavMeshAgent navMeshAgent;
    MonsterPathfinding monsterPathfinding;
    MonsterAnimation monsterAnimation;

    bool standingAttack = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterPathfinding = GetComponent<MonsterPathfinding>();
        monsterAnimation = GetComponent<MonsterAnimation>();
        //attackCooldownCounter = attackCooldown;
    }

    void Update()
    {
        CheckStrikingDistance();
    }

    void CheckStrikingDistance() 
    {
        if (monsterPathfinding.CanSeePlayer()) 
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= (navMeshAgent.stoppingDistance + strikingDistanceBuffer))
                {
                    if (monsterPathfinding.GetIsChasing())
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
        else 
        {
            if (standingAttack) // Don't want this code segment triggering when not concerned with attacking.
            {
                standingAttack = false;
                monsterPathfinding.SetPathfindingOn(false);
            }
        }
    }

    public void Attack() 
    {
        AttackSound();
        FindObjectOfType<Health>().TakeDamage(damage);
        monsterAnimation.PlayAttack();

        standingAttack = true;
        monsterPathfinding.SetPathfindingOn(true);
    }

    void AttackSound() 
    {
        Instantiate(stabSound, transform.position, Quaternion.identity);
    }

}
