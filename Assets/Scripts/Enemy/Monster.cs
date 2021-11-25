using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 5;
    public float waitTime = 0.3f;
    public float turnSpeed = 90f;

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;

    public Transform pathHolder;
    Transform player;
    Color originalSpotlightColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColor = spotLight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) 
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartPatrolling(waypoints);
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;
            StopAllCoroutines();
        }
        else 
        {
            spotLight.color = originalSpotlightColor;
        }
    }

    public bool CanSeePlayer() 
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance) 
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) 
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask)) 
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] waypoints) 
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

    void StartPatrolling(Vector3[] points) 
    {
        StartCoroutine(FollowPath(points));
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
    }
}
