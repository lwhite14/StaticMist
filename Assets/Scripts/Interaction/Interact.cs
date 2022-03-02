using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float rayRange = 4f;
    Camera playerCamera;

    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

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

        Vector3 fwd = playerCamera.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(playerCamera.transform.position, fwd * 50, Color.green);
        bool hit = Physics.Raycast(playerCamera.transform.position, fwd, out hitInfo, 50);

        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            Debug.Log(hitObject.name);
            try
            {
                hitObject.GetComponent<IInteractable>().Interact();
            }
            catch { }      
        }
    }

    void CastDialogueRays()
    {
        bool hit = Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), rayRange);
        if (!hit)
        {
            if (FindObjectOfType<DialogueManager>() != null)
            {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }
    }
}