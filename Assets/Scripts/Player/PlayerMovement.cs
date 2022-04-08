using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustable Variables")]
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpCoolDown = 1f;
    public float movementSliding = 4f;

    [Header("Ground Check Variables")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Head Bob Variables
    float defaultYPos = 0;
    float timer;

    CharacterController controller;
    JumpCoolDownSlider jumpCoolDownSlider;
    PlayerCrouching playerCrouching;

    Camera playerCamera;
    Vector3 velocity;
    Vector3 move;
    bool previousGrounded = true;
    bool hasLanded = false;
    bool canMove = false;
    float speed;
    float bobSpeed;
    float bobAmount;
    float x = 0;
    float z = 0;
    float jumpCoolDownCounter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpCoolDownSlider = GameObject.FindObjectOfType<JumpCoolDownSlider>();
        playerCrouching = GetComponent<PlayerCrouching>();


        jumpCoolDownCounter = jumpCoolDown;
        jumpCoolDownSlider.SetMaxValue(jumpCoolDown);


        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
        Ground();
        JumpCoolDown();
        Move();
        Fall();
        HandleHeadBob();
    }

    void Ground()
    {
        if (!previousGrounded && CheckGrounded()) 
        {
            hasLanded = true;
            jumpCoolDownSlider.SetSliding(true);
        } 
        // Checks if there is a change in 'grounded' state and changes the hasLanded boolean accordingly.

        if (CheckGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        previousGrounded = CheckGrounded();
        // Stores the last frames grounded state. 
    }

    void JumpCoolDown() 
    {
        if (hasLanded) 
        {
            jumpCoolDownCounter -= Time.deltaTime;
            jumpCoolDownSlider.ChangeValue(jumpCoolDownCounter);
            if (jumpCoolDownCounter <= 0) 
            {
                hasLanded = false;
                jumpCoolDownSlider.SetSliding(false);
                jumpCoolDownSlider.ChangeValue(jumpCoolDown);
                jumpCoolDownCounter = jumpCoolDown;
            }
        }
        // hasLanded is true when the player just lands. hasLanded will be true as long as the jump cooldown meter is above 0.
        // This is important as the hasLanded bool stops moving or jumping will the cooldown is up.
        // UI elements and counters are reset when 'jumpCoolDownCounter <= 0'. 
    }

    void Move()
    {
        if (CheckGrounded())
        {
            if (hasLanded || canMove)
            {
                x = 0;
                z = 0;
            }
            //else 
            //{
            //    x = Input.GetAxis("Horizontal");
            //    z = Input.GetAxis("Vertical");
            //}
            move = transform.right * x + transform.forward * z;
        }
        controller.Move(move * speed * Time.deltaTime);
        // If grounded and isn't on jump cooldown; the player moves according to input.
        // If mid-air, the player continues their trajectory.
        // If the player is on jump cooldown; they are rooted for the time being.
    }

    void Fall()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleHeadBob() 
    {
        if (!CheckGrounded()) 
        {
            return;
        }

        if (Mathf.Abs(move.x) > 0.1 || Mathf.Abs(move.z) > 0.1) 
        {
            timer += Time.deltaTime * bobSpeed;
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * bobAmount,
                playerCamera.transform.localPosition.z
            );
        }
    }

    public void JumpInput() 
    {
        Jump();
    }

    void Jump() 
    {
        if (CheckGrounded() && (CheckMovingForward() || CheckNotMoving()) && !hasLanded && !playerCrouching.GetIsCrouching() && !canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public bool CheckGrounded()
    {
        if (groundCheck != null)
        {
            return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        else 
        {
            return true;
        }
    }

    public bool CheckMovingForward() 
    {
        if ((z > 0) && (x == 0))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool CheckNotMoving() 
    {
        if ((z == 0) && (x == 0)) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    public void ChangeBob(float newBobAmount, float newBobSpeed) 
    {
        bobAmount = newBobAmount;
        bobSpeed = newBobSpeed;
    }

    public void MovementSlideX(float newX) 
    {
        if (CheckGrounded())
        {
            if (!hasLanded && !canMove)
            {
                x = Mathf.Lerp(x, newX, movementSliding * Time.deltaTime);
                if (x < newX)
                {
                    if ((x + 0.05) > newX)
                    {
                        SetX(newX);
                    }
                }
                if (x > newX)
                {
                    if ((x - 0.05) < newX)
                    {
                        SetX(newX);
                    }
                }
            }
        }
    }

    public void MovementSlideZ(float newZ)
    {
        if (CheckGrounded())
        {
            if (!hasLanded && !canMove)
            {
                z = Mathf.Lerp(z, newZ, movementSliding * Time.deltaTime);
                if (z < newZ)
                {
                    if ((z + 0.05) > newZ)
                    {
                        SetZ(newZ);
                    }
                }
                if (z > newZ)
                {
                    if ((z - 0.05) < newZ)
                    {
                        SetZ(newZ);
                    }
                }
            }
        }
    }

    public void SetX(float newX) 
    {
        x = newX;
    }

    public void SetZ(float newZ) 
    {
        z = newZ;
    }

    public float GetX() 
    {
        return x;
    }
    
    public float GetZ() 
    {
        return z;
    }

    public void SetIsInMenu(bool newIsInMenu) 
    {
        canMove = newIsInMenu;
    }

    public void WarpToPosition(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;
    } //   ---   Movement Demo Only
}
