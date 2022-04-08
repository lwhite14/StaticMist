using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float rayRange = 4f;
    Camera playerCamera;
    Animator crossHairAnimator;

    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        crossHairAnimator = GameObject.Find("Crosshair").GetComponent<Animator>();
    }

    void Update()
    {
        UpdateRays();
    }

    public void InteractInput() 
    {
        if (!InventoryUI.isOn)
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

    void UpdateRays()
    {
        RaycastHit hitInfo = new RaycastHit();

        Vector3 fwd = playerCamera.transform.TransformDirection(Vector3.forward);
        bool hit = Physics.Raycast(playerCamera.transform.position, fwd, out hitInfo, rayRange);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<IInteractable>() != null)
            {
                crossHairAnimator.SetBool("isOpen", true);
            }
            else 
            {
                IsNotAimingAtInteractable();
            }
        }
        else 
        {
            IsNotAimingAtInteractable();
        }
    }

    void IsNotAimingAtInteractable() 
    {
        crossHairAnimator.SetBool("isOpen", false);
        if (DialogueManager.instance.isTalkingNPC)
        {
            DialogueManager.instance.EndDialogue();
        }
    } // Executed when the player aims off an interactable object.
}