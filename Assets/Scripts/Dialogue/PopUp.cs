using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public float onScreenTime = 5.0f;
    public bool showOnce = true;
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (!isTriggered)
            {
                DialogueTrigger.StopAllDialogue();
                Triggered();
            }
        }
    }

    void Triggered()
    {
        if (showOnce)
        {
            isTriggered = true;
        }
        GetComponent<DialogueTrigger>().StartPopUp();
    }
}
