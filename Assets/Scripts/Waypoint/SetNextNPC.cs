using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetNextNPC : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    public Transform nextTarget;
    public Waypoint waypoint;
    public Animator imageAnim;

    public UnityEvent dialogueEnd;

    bool previousIsTriggered = false;

    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        if (previousIsTriggered && !dialogueTrigger.isTriggered) 
        {
            dialogueEnd.Invoke();
        }
        previousIsTriggered = dialogueTrigger.isTriggered;
    }

    public void SetNextWaypoint() 
    {
        imageAnim.SetBool("disappearQuick", true);
        waypoint.target = nextTarget;
    }

    public void WaypointDissappear() 
    {
        waypoint.target = null;
    }
}
// Used on NPCs, changes the target of the waypoint marker. 
// (May not be used in the final version)
