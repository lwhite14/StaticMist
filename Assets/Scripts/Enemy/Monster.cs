using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Patrol Variables/Objects")]
    public Transform pathHolder;
    public float speed = 5;
    public float waitTime = 0.3f;
    public float turnSpeed = 90f;

    [Header("Sight Variables/Objects")]
    public float spotAngle = 80f;
    public float viewDistance;
    public LayerMask viewMask;

    [Header("Nav Variables/Objects")]
    public NavMeshAgent navMeshAgent;
    public float notVisibleTime = 2f;

    Transform player;
    Vector3[] waypoints;
    Vector3 startWaypoint;
    [HideInInspector]public bool isChasing = false;
    float notVisibleTimeCounter;

    void Start()
    {
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
        if (CanSeePlayer())
        {
            StopAllCoroutines();
            isChasing = true;
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
                    isChasing = false;
                    notVisibleTimeCounter = notVisibleTime;
                    StartCoroutine(ReturnToPatrol());
                }
            }
        }
    }

    public bool CanSeePlayer() 
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance) 
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < spotAngle / 2f) 
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
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
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

    IEnumerator ReturnToPatrol() 
    {
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

    void OnDrawGizmos()
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
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

        Quaternion leftRayRotation = Quaternion.AngleAxis(-(spotAngle / 2.0f), Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis((spotAngle / 2.0f), Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewDistance);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewDistance);
    }
}
