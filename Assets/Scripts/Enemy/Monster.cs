using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Patrol Variables/Objects")]
    public Transform pathHolder;
    public float speed = 5;
    public float patrolTurnWaitTime = 0.3f;
    public float turnSpeed = 90f;
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

    [Header("Unity Events")]
    public UnityEvent playerSpotted;
    public UnityEvent playerLost;


    public UnityEvent idle;
    public UnityEvent patrol;
    public UnityEvent chase;

    NavMeshAgent navMeshAgent;
    Transform player;
    Vector3[] waypoints;
    Vector3 startWaypoint;
    bool isChasing = false;
    bool isDead = false;
    bool isCalled = false;
    float notVisibleTimeCounter;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notVisibleTimeCounter = notVisibleTime;

        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) 
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        startWaypoint = waypoints[0];

        StartCoroutine(FollowPath());
    }

    void Update()
    {
        if (CanSeePlayer() && !isDead)
        {
            StopAllCoroutines();
            navMeshAgent.isStopped = false;
            isChasing = true;
            if (!isCalled) // Makes sure 'playerSpotted' is only invoked once.
            {
                playerSpotted.Invoke();
                chase.Invoke();
                isCalled = true;
            }
        }

        if (isChasing) 
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
                    //isChasing = false;
                    //notVisibleTimeCounter = notVisibleTime;
                    StartCoroutine(WaitForTime());
                }
            }
        }
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

    IEnumerator FollowPath() 
    {

        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetWaypoint) <= buffer)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(patrolTurnWaitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) 
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle))) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    IEnumerator WaitForTime()
    {
        idle.Invoke();
        isChasing = false;
        notVisibleTimeCounter = notVisibleTime;
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(afterChaseWaitTime);
        playerLost.Invoke();
        patrol.Invoke();
        isCalled = false; // Resets 'icCalled' so that Unity event is only called once in update.
        yield return StartCoroutine(ReturnToPatrol());
    }

    public IEnumerator ReturnToPatrol() 
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = startWaypoint;

        while (true)
        {
            float dist = navMeshAgent.remainingDistance;
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        navMeshAgent.isStopped = true;
                        yield return StartCoroutine(FollowPath());
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
            //Gizmos.DrawRay(transform.position, transform.forward * viewDistanceShort);

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
