using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprinting : MonoBehaviour
{
    [Header("Adjustable Variables")]
    public float crouchingSpeed = 3f;
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float normalFov = 70f;
    public float runningFov = 80f;
    [Range(10f, 400f)]public float fovChangeSpeed = 100f;
    public float runningMeter = 8f;
    public float fatiguedCooldown = 2f;
    public float runningMeterFillSpeed = 4f;

    [Header("Other Objects/Components")]
    public RunSlider runSlider;
    public Camera cam;
    public PlayerMovement playerMovement;
    public PlayerCrouching playerCrouching;
    public Footsteps footsteps;

    bool isRunning = false;
    bool runCounterDepleted = false;
    float runningMeterCounter;
    float fatiguedCooldownCounter;

    void Start()
    {
        playerMovement.ChangeSpeed(walkingSpeed);
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
            if (Input.GetButton("Fire3"))
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
        ChangeFovToHigh();
        footsteps.SpeedUpSteps();

        fatiguedCooldownCounter = fatiguedCooldown;
    }
    // Run commands when the speed is increased.
    
    void ChangeSpeedToLow() 
    {
        if (playerCrouching.GetIsCrouching())
        {
            playerMovement.ChangeSpeed(crouchingSpeed);
        }
        else
        {
            playerMovement.ChangeSpeed(walkingSpeed);
        }
        footsteps.SlowDownSteps();
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

}
