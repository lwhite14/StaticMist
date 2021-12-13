using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Transform start;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerMovement>().WarpToPosition(start.position);
    }
}
// Resets the player when they hit a collider. 
// (May not be used in the final version)
