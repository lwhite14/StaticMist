using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    public Monster monster;

    void FixedUpdate()
    {
        if (monster.CanSeePlayer())
        {
            agent.destination = goal.position;
        }
    }
}
