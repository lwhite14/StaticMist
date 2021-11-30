using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    bool isDead = false;

    void Start()
    {
        SetCursorMode(true);
    }

    void Update()
    {
        if (!isDead)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
    // Rotates the camera and parent gameobject to the cursor.

    public void SetMouseSensitivity(float newSens) 
    {
        mouseSensitivity = newSens;
    }

    public void OnDeath(bool newIsDead) 
    {
        isDead = newIsDead;
        if (newIsDead) 
        {
            SetCursorMode(false);
        }
    }

    void SetCursorMode(bool lockedMode) 
    {
        if (lockedMode) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
