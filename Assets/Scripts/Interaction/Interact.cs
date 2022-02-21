using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float rayRange = 4f;

    void Update()
    {
        CastDialogueRays();
    }

    public void InteractInput() 
    {
        if (!FindObjectOfType<InventoryUI>().GetIsOn())
        {
            CastInteractRay();
        }
    }

    void CastInteractRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            try
            {
                hitObject.GetComponent<IInteractable>().Interact();
            }
            catch { }      
        }
    }

    void CastDialogueRays()
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), rayRange);
        if (!hit)
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}