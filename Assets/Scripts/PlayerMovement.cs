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
    public float runningMeter = 8f;
    public float fatiguedCooldown = 2f;
    public float runningMeterFillSpeed = 4f;

    [Header("Other Objects/Componenets")]
    public CharacterController controller;
    public Camera cam;
    public JumpCoolDownSlider jumpCoolDownSlider;
    public RunSlider runSlider;
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
    bool runCounterDepleted = false;
    float x;
    float z;
    float jumpCoolDownCounter;
    float runningMeterCounter;
    float fatiguedCooldownCounter;

    void Start()
    {
        speed = walkingSpeed;
        jumpCoolDownCounter = jumpCoolDown;
        jumpCoolDownSlider.SetMaxValue(jumpCoolDown);
        runningMeterCounter = runningMeter;
        runSlider.SetMaxValue(runningMeter);
        fatiguedCooldownCounter = fatiguedCooldown;
    }

    void Update()
    {
        CheckGrounded();
        JumpCoolDown();
        Move();
        Jump();
        CheckRunningInput();
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

    void CheckRunningInput() 
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
    }

    void ChangeRunning() 
    {
        if (isRunning && CheckMovingForward() && !runCounterDepleted)
        {
            speed = runningSpeed;
            playerAnimations.SetIsRunning(true);

            ChangeFovToHigh();

            fatiguedCooldownCounter = fatiguedCooldown;

            runSlider.ChangeValueDeplete(runningMeterCounter);

            runningMeterCounter -= Time.deltaTime;
            if (runningMeterCounter <= 0) 
            {
                runCounterDepleted = true;
            }
        }
        else 
        {
            speed = walkingSpeed;
            playerAnimations.SetIsRunning(false);

            ChangeFovToLow();

            if (fatiguedCooldownCounter <= 0)
            {
                runCounterDepleted = false;
                if (runningMeterCounter <= runningMeter)
                {
                    runningMeterCounter += Time.deltaTime * runningMeterFillSpeed;
                }
                else
                {
                    runningMeterCounter = runningMeter;
                }
                runSlider.ChangeValueAdd(runningMeterCounter);
            }
            else 
            {
                fatiguedCooldownCounter -= Time.deltaTime;
            }
        }
    }

    void ChangeFovToHigh() 
    {
            if (cam.fieldOfView < runningFov)
            {
                cam.fieldOfView += fovChangeSpeed * Time.deltaTime;
            }
            else 
            {
                cam.fieldOfView = runningFov;
            }
    }

    void ChangeFovToLow()
    {
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
