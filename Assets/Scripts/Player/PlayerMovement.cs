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

        if (CheckGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        previousGrounded = CheckGrounded();
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
    }

    void Jump() 
    {

        if (Input.GetButtonDown("Jump") && CheckGrounded() && (CheckMovingForward() || CheckNotMoving()) && !hasLanded && !playerCrouching.GetIsCrouching())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
