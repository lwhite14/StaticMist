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
    public Transform[] pathHolder;
    public float patrolSpeed = 5;
    public float buffer = 0.05f;
    public float stopChance = 0.25f;

    [Header("Sight Variables/Objects")]
    public float spotAngleLong = 80f;
    public float viewDistanceLong;
    public float spotAngleShort = 180f;
    public float viewDistanceShort;
    public float viewDistanceVeryShort;
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

        RandomPath();
        
        StartCoroutine(InitFollowPath());
    }

    void Update()
    {
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        if (!SettingsMenu.paused)
        {
            if (Time.deltaTime > 0)
            {
                speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
                lastPosition = transform.position;
                monsterAnimationSound.SetSpeed(speed);
            }
        }
    }

    void RandomPath() 
    {
        System.Random rnd = new System.Random();
        int randIndex = rnd.Next(0, pathHolder.Length);

        waypoints = new Vector3[pathHolder[randIndex].childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder[randIndex].GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        startWaypoint = waypoints[0];
    }

    bool IsRandomStop() 
    {
        System.Random random = new System.Random();
        double val = random.NextDouble();
        if ((float)val < stopChance) 
        {
            return true;
        }
        return false;
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

    public bool CanSeePlayerClose() 
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistanceVeryShort)
        {
            if (!Physics.Linecast(transform.position, player.position, viewMask))
            {
                return true;
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
                if (((targetWaypointIndex + 1) % waypoints.Length) == 0) 
                {
                    RandomPath();
                }
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                if (IsRandomStop())
                {
                    yield return StartCoroutine(RandomStop());
                }
            }
            if (CanSeePlayerClose() && !isDead)
            {
                yield return StartCoroutine(ChasePlayer());
            }
            if (CanSeePlayer() && !isDead)
            {
                yield return StartCoroutine(Investigating());
            }
            yield return null;
        }
    }

    IEnumerator RandomStop() 
    {
        navMeshAgent.isStopped = true;
        System.Random random = new System.Random();
        double stopTime = (random.NextDouble() * (3.0 - 0.6) + 0.6);

        //yield return new WaitForSeconds((float)stopTime);
        float timer = 0;
        while (timer < (float)stopTime)
        {
            timer = timer + Time.deltaTime;
            if (CanSeePlayerClose() && !isDead)
            {
                yield return StartCoroutine(ChasePlayer());
            }
            if (CanSeePlayer() && !isDead)
            {
                yield return StartCoroutine(Investigating());
            }
            yield return null;
        }

        navMeshAgent.isStopped = false;
        yield return StartCoroutine(FollowPath());
    }

    IEnumerator Investigating() 
    {
        navMeshAgent.isStopped = true;
        monsterAnimationSound.PlayerSpottedStab();

        while (true) 
        {
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSmoothing);

            if (CanSeePlayer() && !isDead)
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
            if (CanSeePlayerClose() && !isDead)
            {
                ResetInvestigatingVariables();
                yield return StartCoroutine(ChasePlayer());
            }
            yield return null;
        }
    }

    void ResetInvestigatingVariables() 
    {
        investigatingTimerCounter = investigatingTime;
        outOfSightTimeCounter = outOfSightTime;
    }

    public IEnumerator ChasePlayer() 
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = player.position;
        MusicManager.instance.SwitchToChase();
        monsterAnimationSound.SwitchToChase();
        NPC.PlayerChased();
        isChasing = true;

        while (true)
        {
            navMeshAgent.destination = player.position;
            if (CanSeePlayer() && !isDead)
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

        //yield return new WaitForSeconds(afterChaseWaitTime);
        float timer = 0;
        while (timer < afterChaseWaitTime) 
        {
            timer = timer + Time.deltaTime;
            if (CanSeePlayer() && !isDead)
            {
                yield return StartCoroutine(ChasePlayer());
            }
            yield return null;
        }

        MusicManager.instance.SwitchToTense();
        NPC.PlayerEscaped();
        yield return StartCoroutine(ReturnToPatrol());
    }

    public IEnumerator ReturnToPatrol() 
    {
        // At this point the player has evaded the monster, and so I send an event to Unity Analytics.
        AnalyticsFunctions.PlayerEscape(this);

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
            if (CanSeePlayerClose() && !isDead)
            {
                yield return StartCoroutine(ChasePlayer());
            }
            if (CanSeePlayer() && !isDead)
            {
                yield return StartCoroutine(Investigating());
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

    public void SetIsChasing(bool isChasing)
    {
        this.isChasing = isChasing;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetPathfindingOn(bool isOn) 
    {
        navMeshAgent.isStopped = isOn;
    }

    void OnDrawGizmos()
    {
        try
        {
            foreach (Transform singlePathHolder in pathHolder)
            {
                Vector3 startPos = singlePathHolder.GetChild(0).position;
                Vector3 prevPos = startPos;
                foreach (Transform waypoint in singlePathHolder)
                {
                    Gizmos.DrawSphere(waypoint.position, 0.3f);
                    Gizmos.DrawLine(prevPos, waypoint.position);
                    prevPos = waypoint.position;
                }
                Gizmos.DrawLine(prevPos, startPos);
            }

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


            Gizmos.color = Color.cyan;

            Quaternion leftRayRotationVeryShort = Quaternion.AngleAxis(-(360 / 2.0f), Vector3.up);
            Quaternion rightRayRotationVeryShort = Quaternion.AngleAxis((360 / 2.0f), Vector3.up);
            Vector3 leftRayDirectionVeryShort = leftRayRotationVeryShort * transform.forward;
            Vector3 rightRayDirectionVeryShort = rightRayRotationVeryShort * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirectionVeryShort * viewDistanceVeryShort);
            Gizmos.DrawRay(transform.position, rightRayDirectionVeryShort * viewDistanceVeryShort);
        }
        catch 
        {
            Debug.Log("Gizmos only work within the scene view, not the prefab view!");
        }
    }
}
