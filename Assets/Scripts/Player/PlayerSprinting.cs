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
    public PlayerAnimations playerAnimations;
    public PlayerMovement playerMovement;
    public PlayerCrouching playerCrouching;

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
    }

    void ChangeSpeedToHigh() 
    {
        playerMovement.ChangeSpeed(runningSpeed);
        playerAnimations.SetIsRunning(true);
        ChangeFovToHigh();

        fatiguedCooldownCounter = fatiguedCooldown;
    }    
    
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
        playerAnimations.SetIsRunning(false);
        ChangeFovToLow();
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
        if (cam.fieldOfView > normalFov)
        {
            cam.fieldOfView -= fovChangeSpeed * Time.deltaTime;
        }
        else
        {
            cam.fieldOfView = normalFov;
        }
    }

}
