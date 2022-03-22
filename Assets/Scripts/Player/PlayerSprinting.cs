using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprinting : MonoBehaviour
{
    [Header("Adjustable Variables")]
    public float runningMeter = 8f;
    public float fatiguedCooldown = 2f;
    public float runningMeterFillSpeed = 4f;

    [Header("FOV")]
    public float normalFov = 70f;
    public float runningFov = 80f;
    [Range(10f, 400f)] public float fovChangeSpeed = 100f;

    [Header("Speeds")]
    public float crouchingSpeed = 3f;
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;

    [Header("Head Bob Amounts")]
    public float crouchBobSpeed = 8f;
    public float crouchBobAmount = 0.25f;

    public float walkBobSpeed = 14f;
    public float walkBobAmount = 0.5f;

    public float runningBobSpeed = 18f;
    public float runningBobAmount = 1f;

    RunSlider runSlider;
    Camera cam;
    PlayerMovement playerMovement;
    PlayerCrouching playerCrouching;
    Footsteps footsteps;

    bool inputPressed = false;
    bool isRunning = false;
    bool runCounterDepleted = false;
    float runningMeterCounter;
    float fatiguedCooldownCounter;

    void Start()
    {
        runSlider = GameObject.FindObjectOfType<RunSlider>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCrouching = GetComponent<PlayerCrouching>();
        footsteps = GetComponent<Footsteps>();

        playerMovement.ChangeSpeed(walkingSpeed);
        playerMovement.ChangeBob(walkBobAmount, walkBobSpeed);
        runningMeterCounter = runningMeter;
        runSlider.SetMaxValue(runningMeter);
        fatiguedCooldownCounter = fatiguedCooldown;
    }
    void Update()
    {
        CheckRunningInput();
        ChangeRunning();
    }

    void CheckRunningInput()
    {
        if (playerMovement.CheckGrounded())
        {
            if (inputPressed)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        }
        // Nested if statements as I don't want the isRunning value to change if the player is in the air. 
    }

    void ChangeRunning()
    {
        if (isRunning && playerMovement.CheckMovingForward() && !runCounterDepleted && !playerCrouching.GetIsCrouching())
        {
            ChangeSpeedToHigh();

            runningMeterCounter -= Time.deltaTime;
            runSlider.ChangeValue(runningMeterCounter);
            if (runningMeterCounter <= 0)
            {
                runCounterDepleted = true;
            }
        } 
        // Can change to high if player isn't crouching or the run meter isn't depleted.
        // If the run meter runs out, the appropriate bool is changed to false. 
        else
        {
            ChangeSpeedToLow();

            if (runningMeterCounter < runningMeter)
            {
                if (fatiguedCooldownCounter <= 0)
                {

                    runCounterDepleted = false;
                    runningMeterCounter += Time.deltaTime * runningMeterFillSpeed;
                    runSlider.ChangeValue(runningMeterCounter);
                }
                else
                {
                    fatiguedCooldownCounter -= Time.deltaTime;
                }
            }
            else 
            { 
                runningMeterCounter = runningMeter;
            }
        }
        // Has to walk whilst the fatiguedCooldownCounter is above 0.
        // When is goes below, the running meter begins to fill again, and the player is allowed to run again.
        // When its full, all the if statements stop running altogether. 
    }

    void ChangeSpeedToHigh() 
    {
        playerMovement.ChangeSpeed(runningSpeed);
        playerMovement.ChangeBob(runningBobAmount, runningBobSpeed);
        ChangeFovToHigh();
        if (footsteps != null)
        {
            footsteps.SpeedUpSteps();
        }

        fatiguedCooldownCounter = fatiguedCooldown;
    }
    // Run commands when the speed is increased.
    
    void ChangeSpeedToLow() 
    {
        if (playerCrouching.GetIsCrouching())
        {
            playerMovement.ChangeSpeed(crouchingSpeed);
            playerMovement.ChangeBob(crouchBobAmount, crouchBobSpeed);
        }
        else
        {
            playerMovement.ChangeSpeed(walkingSpeed);
            playerMovement.ChangeBob(walkBobAmount, walkBobSpeed);
        }
        if (footsteps != null)
        {
            footsteps.SlowDownSteps();
        }
        ChangeFovToLow();
    }
    // Run commands when the speed is decreased.

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
    // Game-feel FOV change.

    void ChangeFovToLow()
    {
        if (cam.fieldOfView > normalFov)
        {
            cam.fieldOfView -= fovChangeSpeed * Time.deltaTime;
        }
        else
        {
            cam.fieldOfView = normalFov;
        }
    }
    // Game-feel FOV change.

    public void SprintInput(bool newInputPressed) 
    {
        inputPressed = newInputPressed;
    }

}
