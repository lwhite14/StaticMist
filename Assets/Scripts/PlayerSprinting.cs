using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprinting : MonoBehaviour
{
    [Header("Adjustable Variables")]
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float walkingFov = 70f;
    public float runningFov = 80f;
    [Range(10f, 400f)] public float fovChangeSpeed = 100f;
    public float runningMeter = 8f;
    public float fatiguedCooldown = 2f;
    public float runningMeterFillSpeed = 4f;

    [Header("Other Objects/Components")]
    public RunSlider runSlider;
    public Camera cam;
    public PlayerAnimations playerAnimations;
    public PlayerMovement playerMovement;

    bool isRunning = false;
    bool runCounterDepleted = false;
    [HideInInspector] public float speed;
    float runningMeterCounter;
    float fatiguedCooldownCounter;

    void Start()
    {
        speed = walkingSpeed;
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
        if (isRunning && playerMovement.CheckMovingForward() && !runCounterDepleted)
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
        speed = runningSpeed;
        playerAnimations.SetIsRunning(true);
        ChangeFovToHigh();

        fatiguedCooldownCounter = fatiguedCooldown;
    }    
    
    void ChangeSpeedToLow() 
    {
        speed = walkingSpeed;
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
