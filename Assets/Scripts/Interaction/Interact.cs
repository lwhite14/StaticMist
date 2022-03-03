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
        Debug.DrawRay(playerCamera.transform.position, fwd * rayRange, Color.green);
        bool hit = Physics.Raycast(playerCamera.transform.position, fwd, out hitInfo, rayRange);

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
        Vector3 fwd = playerCamera.transform.TransformDirection(Vector3.forward);
        bool hit = Physics.Raycast(playerCamera.transform.position, fwd, rayRange);
        if (!hit)
        {
            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.EndDialogue();
            }
        }
    }
}