using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpotted : MonoBehaviour
{
    public float rayRange = 12f;
    public float spotAngle = 40f;
    public float musicalStabResetTime = 60f;
    public LayerMask obstacleMask;
    float musicalStabResetTimeCounter = 0;
    Transform player;

    public bool hasSpotted { get; private set; } = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CastLines();
    }

    void CastLines() 
    {
        if (Vector3.Distance(transform.position, player.position) < rayRange)
        {
            Vector3 dirToPlayer = (transform.position - player.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(player.transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < spotAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, obstacleMask))
                {
                    if (musicalStabResetTimeCounter <= 0)
                    {
                        GetComponent<MonsterAnimationAndSound>().MonsterSpottedStab();
                        hasSpotted = true;
                    }
                    musicalStabResetTimeCounter = musicalStabResetTime;
                }
            }
        }

        if (musicalStabResetTimeCounter > 0) 
        {
            musicalStabResetTimeCounter -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(player.position, player.forward * rayRange);

            Quaternion leftRayRotationShort = Quaternion.AngleAxis(-(spotAngle / 2.0f), Vector3.up);
            Quaternion rightRayRotationShort = Quaternion.AngleAxis((spotAngle / 2.0f), Vector3.up);
            Vector3 leftRayDirectionShort = leftRayRotationShort * player.forward;
            Vector3 rightRayDirectionShort = rightRayRotationShort * player.forward;
            Gizmos.DrawRay(player.position, leftRayDirectionShort * rayRange);
            Gizmos.DrawRay(player.position, rightRayDirectionShort * rayRange);
        }
        catch { }
    }
}
