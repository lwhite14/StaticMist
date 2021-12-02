using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public float stepInterval = 0.5f;
    public GameObject stepOne;
    public GameObject stepTwo;

    PlayerMovement playerMovement;

    float stepIntervalCounter;
    float fastStepInterval;
    float slowStepInterval;
    int stepCount = 0;
    bool previousGrounded = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        stepIntervalCounter = stepInterval;

        fastStepInterval = stepInterval / 2;
        slowStepInterval = stepInterval;
    }

    void Update()
    {
        if (playerMovement.CheckGrounded() && !playerMovement.CheckNotMoving())
        {
            if (stepIntervalCounter <= 0)
            {
                if (stepCount == 0)
                {
                    Instantiate(stepOne, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(stepTwo, transform.position, Quaternion.identity);
                }
                stepIntervalCounter = stepInterval;
                stepCount++;
                if (stepCount == 2)
                {
                    stepCount = 0;
                }
            }
            stepIntervalCounter -= Time.deltaTime;
        }
        else 
        {
            stepIntervalCounter = stepInterval;
        }

        if (!previousGrounded && playerMovement.CheckGrounded())
        {
            Land();
        }
        previousGrounded = playerMovement.CheckGrounded();
    }

    void Land() 
    {
        Instantiate(stepOne, transform.position, Quaternion.identity);
    }

    public void SpeedUpSteps() 
    {
        stepInterval = fastStepInterval;
    }

    public void SlowDownSteps() 
    {
        stepInterval = slowStepInterval;
    }
}
