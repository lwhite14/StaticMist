using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    [Range(1.2f, 4f)]public float standingHeight = 1.6f;
    [Range(0.4f, 2f)]public float crouchingHeight = 0.8f;
    public float crouchChangeSpeed = 1f;

    public PlayerMovement playerMovement;
    public CharacterController controller;
    public Transform groundChecker;

    bool isCrouching = false;

    void Update()
    {
        CheckCrouchingInput();
        ChangeCrouching();
    }

    void CheckCrouchingInput()
    {
        if (Input.GetButtonDown("Movement3"))
        {
            if (!isCrouching)
            {
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
        } 
    }

    void ChangeCrouching() 
    {
        if (isCrouching)
        {
            if (controller.height > crouchingHeight)
            {
                controller.height -= Time.deltaTime * crouchChangeSpeed;
                groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, groundChecker.localPosition.y + ((Time.deltaTime * crouchChangeSpeed)/2), groundChecker.localPosition.z);
            }
            else 
            {
                controller.height = crouchingHeight;
                groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, -(crouchingHeight / 2), groundChecker.localPosition.z);
            }
        }
        else 
        {
            if (controller.height < standingHeight)
            {
                controller.height += Time.deltaTime * crouchChangeSpeed;
                groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, groundChecker.localPosition.y - ((Time.deltaTime * crouchChangeSpeed) / 2), groundChecker.localPosition.z);
            }
            else 
            {
                controller.height = standingHeight;
                groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, -(standingHeight / 2), groundChecker.localPosition.z);
            }
        }
    }

    public bool GetIsCrouching() 
    {
        return isCrouching;
    }

}
