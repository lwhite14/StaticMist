using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float rayRange = 4f;

    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (Input.GetButtonDown("Interact"))
            {
                hitObject.GetComponent<IInteractable>().Interact();
            }
        }
        else 
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
    // If the raycast hits, the interactable object has its interact() funtion invoked. 
    // If not the enddialogue method is invoked (used if the player turns away from a conversation).
}