using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed;
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float walkingFov = 70f;
    public float runningFov = 80f;
    [Range(10f, 400f)]
    public float fovChangeSpeed = 100f;
    public float jumpCoolDown = 1f;

    [Header("Other Objects/Componenets")]
    public CharacterController controller;
    public Camera cam;
    public JumpCoolDownSlider jumpCoolDownSlider;
    public PlayerAnimations playerAnimations;

    [Header("Ground Check Variables")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 move;
    bool isGrounded = true;
    bool hasLanded = false;
    bool isRunning = false;
    float x;
    float z;
    float jumpCoolDownCounter;

    void Start()
    {
        speed = walkingSpeed;
        jumpCoolDownCounter = jumpCoolDown;
        jumpCoolDownSlider.SetMaxValue(jumpCoolDown);
    }

    void Update()
    {
        CheckGrounded();
        JumpCoolDown();
        Move();
        Jump();
        ChangeRunning();
    }

    void CheckGrounded()
    {
        bool tempGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if ((tempGrounded == false) && (isGrounded == true)) 
        {
            hasLanded = true;
            jumpCoolDownSlider.SetSliding(true);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
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
        if (isGrounded)
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

        if (Input.GetButtonDown("Jump") && isGrounded && (CheckMovingForward() || CheckNotMoving()) && !hasLanded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckMovingForward() 
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

    void ChangeRunning() 
    {
        if (isGrounded)
        {
            if (Input.GetButton("Fire3"))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        }


        if (isRunning && CheckMovingForward())
        {
            speed = runningSpeed;
            playerAnimations.SetIsRunning(true);
            if (cam.fieldOfView < runningFov)
            {
                cam.fieldOfView += fovChangeSpeed * Time.deltaTime;
            }
            else 
            {
                cam.fieldOfView = runningFov;
            }
        }
        else 
        {
            speed = walkingSpeed;
            playerAnimations.SetIsRunning(false);
            if (cam.fieldOfView > walkingFov)
            {
                cam.fieldOfView -= fovChangeSpeed * Time.deltaTime;
            }
            else
            {
                cam.fieldOfView = walkingFov;
            }
        }
    }
}
