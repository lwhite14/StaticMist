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

    [Header("Other Objects/Componenets")]
    public CharacterController controller;
    public Camera cam;

    [Header("Ground Check Variables")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float x;
    float z;
    Vector3 move;

    void Start()
    {
        speed = walkingSpeed;
    }

    void Update()
    {
        CheckGrounded(); 

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        ChangeRunning();
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ChangeRunning() 
    {
        if (Input.GetButton("Fire3"))
        {
            speed = runningSpeed;
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
