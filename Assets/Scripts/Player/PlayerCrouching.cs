using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    [Header("Adjustable Variables")]
    [Range(1.2f, 4f)]public float standingHeight = 1.6f;
    [Range(0.4f, 2f)]public float crouchingHeight = 0.8f;
    public float crouchChangeSpeed = 1f;

    [Header("Other Objects/Components")]
    public PlayerMovement playerMovement;
    public CharacterController controller;
    public Transform groundChecker;

    [Header("Celing Check Variables")]
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    public LayerMask ceilingMask;

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
            if (isCrouching && !CheckHeadClear())
            {
                isCrouching = false;
            }
            else
            {
                isCrouching = true;
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
        // Changes the height slowly, so it feels more natural. 
    }

    public bool GetIsCrouching() 
    {
        return isCrouching;
    }

    bool CheckHeadClear()
    {
        return Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);
    }

}
