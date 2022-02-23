using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.Services.Analytics;

public class MonsterPathfinding : MonoBehaviour
{
    public string monsterName;
    public Monster monsterInformation = new Monster();

    [Header("Patrol Variables/Objects")]
    public Transform pathHolder;
    public float patrolSpeed = 5;
    public float buffer = 0.05f;

    [Header("Sight Variables/Objects")]
    public float spotAngleLong = 80f;
    public float viewDistanceLong;
    public float spotAngleShort = 180f;
    public float viewDistanceShort;
    public float afterChaseWaitTime = 4f;
    public LayerMask viewMask;

    [Header("Nav Variables/Objects")]
    public float notVisibleTime = 2f;
    public float chaseSpeed = 6f;

    [Header("Investigating Variables")]
    public float investigatingTime = 2.5f;
    public float outOfSightTime = 3.5f;
    public float rotationSmoothing = 5f;

    NavMeshAgent navMeshAgent;
    MonsterAnimationAndSound monsterAnimationSound;
    Transform player;
    Vector3[] waypoints;
    Vector3 startWaypoint;
    Vector3 lastPosition;
    bool isChasing = false;
    bool isDead = false;
    float notVisibleTimeCounter;
    float speed = 0f;
    float investigatingTimerCounter;
    float outOfSightTimeCounter;
    int targetWaypointIndex;
    Vector3 targetWaypoint;

    void Start()
    {
        monsterInformation.SetName(monsterName);

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.isStopped = false;
        monsterAnimationSound = GetComponent<MonsterAnimationAndSound>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notVisibleTimeCounter = notVisibleTime;
        investigatingTimerCounter = investigatingTime;
        outOfSightTimeCounter = outOfSightTime;

        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        startWaypoint = waypoints[0];

        StartCoroutine(InitFollowPath());
    }

    void Update()
    {
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        monsterAnimationSound.SetSpeed(speed);
    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistanceLong)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < spotAngleLong / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }

        if (Vector3.Distance(transform.position, player.position) < viewDistanceShort)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < spotAngleShort / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator InitFollowPath()
    {
        transform.position = waypoints[0];
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        yield return StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        navMeshAgent.isStopped = false;

        while (true)
        {
            navMeshAgent.destination = targetWaypoint;
            if (Vector3.Distance(transform.position, targetWaypoint) <= buffer)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
            }
            if (CanSeePlayer() && !isDead)
            {
                yield return StartCoroutine(Investigating());
            }
            yield return null;
        }
    }

    IEnumerator Investigating() 
    {
        navMeshAgent.isStopped = true;
        monsterAnimationSound.PlayerSpottedStab();

        while (true) 
        {
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSmoothing);

            if (CanSeePlayer())
            {
                investigatingTimerCounter -= Time.deltaTime;

                if (investigatingTimerCounter <= 0) 
                {
                    ResetInvestigatingVariables();
                    yield return StartCoroutine(ChasePlayer());
                }
            }
            else 
            {
                outOfSightTimeCounter -= Time.deltaTime;
                if (outOfSightTimeCounter <= 0) 
                {
                    ResetInvestigatingVariables();
                    yield return StartCoroutine(FollowPath());
                }
            }
            yield return null;
        }
    }

    void ResetInvestigatingVariables() 
    {
        investigatingTimerCounter = investigatingTime;
        outOfSightTimeCounter = outOfSightTime;
    }

    IEnumerator ChasePlayer() 
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = player.position;
        FindObjectOfType<MusicManager>().SwitchToChase();
        monsterAnimationSound.SwitchToChase();
        isChasing = true;

        while (true)
        {
            navMeshAgent.destination = player.position;
            if (CanSeePlayer())
            {
                notVisibleTimeCounter = notVisibleTime;
            }
            else
            {
                notVisibleTimeCounter -= Time.deltaTime;
                if (notVisibleTimeCounter <= 0)
                {
                    isChasing = false;
                    yield return StartCoroutine(WaitForTime());
                }
            }
            yield return null;
        }
    }

    IEnumerator WaitForTime()
    {
        notVisibleTimeCounter = notVisibleTime;
        navMeshAgent.isStopped = true;
        monsterAnimationSound.SwitchToPassive();
        yield return new WaitForSeconds(afterChaseWaitTime);
        FindObjectOfType<MusicManager>().SwitchToTense();
        yield return StartCoroutine(ReturnToPatrol());
    }

    public IEnumerator ReturnToPatrol() 
    {
        // At this point the player has evaded the monster, and so I send an event to Unity Analytics.
        SendDataToAnalytics();

        navMeshAgent.isStopped = false;
        navMeshAgent.destination = startWaypoint;
        navMeshAgent.speed = patrolSpeed;

        while (true)
        {
            float dist = navMeshAgent.remainingDistance;
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        yield return StartCoroutine(InitFollowPath());
                    }
                }
            }
            yield return null;
        }     
    }

    public void OnDeath(bool newIsDeath) 
    {
        isDead = newIsDeath;
        if (newIsDeath) 
        {
            isChasing = false;
            notVisibleTimeCounter = notVisibleTime;
        }
    }

    public bool GetIsChasing() 
    {
        return isChasing;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetPathfindingOn(bool isOn) 
    {
        navMeshAgent.isStopped = isOn;
    }

    void SendDataToAnalytics() 
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Monster", GetComponent<MonsterPathfinding>().monsterInformation.GetName() }
            };
            Events.CustomData("PlayerEscape", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'PlayerEscape' with: Monster = " + GetComponent<MonsterPathfinding>().monsterInformation.GetName());
        }
    }

    void OnDrawGizmos()
    {
        try
        {
            Vector3 startPos = pathHolder.GetChild(0).position;
            Vector3 prevPos = startPos;
            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, 0.3f);
                Gizmos.DrawLine(prevPos, waypoint.position);
                prevPos = waypoint.position;
            }
            Gizmos.DrawLine(prevPos, startPos);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * viewDistanceLong);

            Quaternion leftRayRotationLong = Quaternion.AngleAxis(-(spotAngleLong / 2.0f), Vector3.up);
            Quaternion rightRayRotationLong = Quaternion.AngleAxis((spotAngleLong / 2.0f), Vector3.up);
            Vector3 leftRayDirectionLong = leftRayRotationLong * transform.forward;
            Vector3 rightRayDirectionLong = rightRayRotationLong * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirectionLong * viewDistanceLong);
            Gizmos.DrawRay(transform.position, rightRayDirectionLong * viewDistanceLong);

            Gizmos.color = Color.blue;

            Quaternion leftRayRotationShort = Quaternion.AngleAxis(-(spotAngleShort / 2.0f), Vector3.up);
            Quaternion rightRayRotationShort = Quaternion.AngleAxis((spotAngleShort / 2.0f), Vector3.up);
            Vector3 leftRayDirectionShort = leftRayRotationShort * transform.forward;
            Vector3 rightRayDirectionShort = rightRayRotationShort * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirectionShort * viewDistanceShort);
            Gizmos.DrawRay(transform.position, rightRayDirectionShort * viewDistanceShort);
        }
        catch 
        {
            Debug.Log("Gizmos only work within the scene view, not the prefab view!");
        }
    }
}
