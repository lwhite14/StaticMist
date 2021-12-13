using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    public Transform groundCheckTransform;
    [Range(0f, 1f)] public float crouchFactor = 0.5f;
    public float crouchSpeed = 5f;

    [Header("Ceilng Check Variables")]
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    public LayerMask ceilingMask;

    CharacterController controller;
    float startHeight;
    float newHeight;
    bool isCrouching = false;
    bool isDead = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        startHeight = controller.height;
        newHeight = startHeight;
    }

    void Update()
    {  
        float lastHeight = controller.height;
        if (controller.height > newHeight)
        {
            if ((controller.height - 0.02) <= newHeight)
            {
                controller.height = newHeight;
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, newHeight, crouchSpeed * Time.deltaTime);
            }
        }
        else if (controller.height < newHeight) 
        {
            if ((controller.height + 0.02) >= newHeight)
            {
                controller.height = newHeight;
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, newHeight, crouchSpeed * Time.deltaTime);
            }
        }

        // fix vertical position
        controller.Move(new Vector3(0, (controller.height - lastHeight) * 0.5f, 0));
        groundCheckTransform.position += new Vector3(0, -(controller.height - lastHeight) * 0.5f, 0);
    }
    // Changes the height slowly, so it feels more natural. 

    public void CrouchInput() 
    {
        if (!isDead)
        {
            if (isCrouching && !CheckHeadClear())
            {
                newHeight = startHeight;
                isCrouching = false;
            }
            else
            {
                newHeight = crouchFactor * startHeight;
                isCrouching = true;
            }
        }
    }
    // Triggered by new input system.

    public bool GetIsCrouching() 
    {
        return isCrouching;
    }

    bool CheckHeadClear()
    {
        return Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);
    }

    public void OnDeath(bool newIsDead)
    {
        isDead = newIsDead;
    }
}
