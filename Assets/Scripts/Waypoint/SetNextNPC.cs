using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextNPC : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    public Transform nextTarget;
    public Waypoint waypoint;
    public Animator imageAnim;

    bool previousIsTriggered = false;

    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        if (previousIsTriggered && !dialogueTrigger.isTriggered) 
        {
            imageAnim.SetBool("disappearQuick", true);
            waypoint.target = nextTarget;
        }
        previousIsTriggered = dialogueTrigger.isTriggered;
    }
}
// Used on NPCs, changes the target of the waypoint marker. 
// (May not be used in the final version)
