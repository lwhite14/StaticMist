using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustable Variables")]
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpCoolDown = 1f;

    [Header("Other Objects/Components")]
    public CharacterController controller;
    public JumpCoolDownSlider jumpCoolDownSlider;
    public PlayerSprinting playerSprinting;
    public PlayerCrouching playerCrouching;

    [Header("Ground Check Variables")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 move;
    bool previousGrounded = true;
    bool hasLanded = false;
    float speed;
    float x;
    float z;
    float jumpCoolDownCounter;

    void Start()
    {
        jumpCoolDownCounter = jumpCoolDown;
        jumpCoolDownSlider.SetMaxValue(jumpCoolDown);
    }

    void Update()
    {
        Ground();
        JumpCoolDown();
        Move();
        Jump();
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
            if (hasLanded)
            {
                x = 0;
                z = 0;
            }
            else 
            {
                x = Input.GetAxis("Horizontal");
                z = Input.GetAxis("Vertical");
            }
            move = transform.right * x + transform.forward * z;
        }
        controller.Move(move * speed * Time.deltaTime);
        // If grounded and isn't on jump cooldown; the player moves according to input.
        // If mid-air, the player continues their trajectory.
        // If the player is on jump cooldown; they are rooted for the time being.
    }

    void Jump() 
    {

        if (Input.GetButtonDown("Jump") && CheckGrounded() && (CheckMovingForward() || CheckNotMoving()) && !hasLanded && !playerCrouching.GetIsCrouching())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // Added vertical velocity as long as the player is grounded, either moving forward or completely still, isnt on cooldown, and isn't crouching.
        // Note: Gravity is added manually.
    }

    public bool CheckGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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

    bool CheckNotMoving() 
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

    public void WarpToPosition(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;
    } //   ---   Movement Demo Only
}
