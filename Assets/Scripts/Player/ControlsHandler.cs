using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHandler : MonoBehaviour
{
    MasterControls controls;
    InputAction look;
    InputAction forwardBackward;
    InputAction lateral;

    MouseLook mouseLook;
    PlayerMovement playerMovement;
    PlayerSprinting playerSprinting;
    PlayerCrouching playerCrouching;
    Interact interact;
    InventoryUI inventoryUI;

    void Awake()
    {
        controls = new MasterControls();
    }

    void Start()
    {
        mouseLook = GetComponentInChildren<MouseLook>();
        playerMovement = GetComponent<PlayerMovement>();
        playerSprinting = GetComponent<PlayerSprinting>();
        playerCrouching = GetComponent<PlayerCrouching>();
        interact = GetComponent<Interact>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    void OnEnable()
    {
        look = controls.Player.Look;
        look.Enable();
        forwardBackward = controls.Player.Walk_ForwardBackwards;
        forwardBackward.Enable();
        lateral = controls.Player.Walk_Laterally;
        lateral.Enable();

        controls.Player.Jump.performed += Jump;
        controls.Player.Jump.Enable();
        controls.Player.Sprint.performed += SprintOn;
        controls.Player.Sprint.canceled += SprintOff;
        controls.Player.Sprint.Enable();
        controls.Player.Crouch.performed += Crouch;
        controls.Player.Crouch.Enable();
        controls.Player.Interact.started += Interact;
        controls.Player.Interact.Enable();

        controls.UI.Exit.performed += Exit;
        controls.UI.Exit.Enable();
        controls.UI.Instructions.performed += Instructions;
        controls.UI.Instructions.Enable();
        controls.UI.SensUp.performed += SensUp;
        controls.UI.SensUp.Enable();
        controls.UI.SensDown.performed += SensDown;
        controls.UI.SensDown.Enable();
        controls.UI.Inventory.performed += Inventory;
        controls.UI.Inventory.Enable();
        

    }

    void OnDisable()
    {
        look.Disable();
        forwardBackward.Disable();
        lateral.Disable();
        controls.Player.Sprint.Disable();
        controls.Player.Jump.Disable();
        controls.Player.Crouch.Disable();
        controls.Player.Interact.Disable();

        controls.UI.Exit.Disable();
        controls.UI.Instructions.Disable();
        controls.UI.SensUp.Disable();
        controls.UI.SensDown.Disable();
        controls.UI.Inventory.Disable();
    }

    void Jump(InputAction.CallbackContext obj)
    {
        playerMovement.JumpInput();
    }

    void SprintOn(InputAction.CallbackContext obj) 
    {
        playerSprinting.SprintInput(true);
    }

    void SprintOff(InputAction.CallbackContext obj) 
    {
        playerSprinting.SprintInput(false);
    }

    void Crouch(InputAction.CallbackContext obj)
    {
        playerCrouching.CrouchInput();
    }

    void Interact(InputAction.CallbackContext obj) 
    {
        interact.InteractInput();
    }

    void Exit(InputAction.CallbackContext obj)
    {
        GameObject.FindObjectOfType<GameManager>().ExitGame();
    }

    void Instructions(InputAction.CallbackContext obj)
    {
        GameObject.FindObjectOfType<ControlsTab>().InstructionsInput();
    }

    void SensUp(InputAction.CallbackContext obj)
    {
        GameObject.FindObjectOfType<ControlsTab>().SensitivityUp();
    }

    void SensDown(InputAction.CallbackContext obj)
    {
        GameObject.FindObjectOfType<ControlsTab>().SensitivityDown();
    }

    void Inventory(InputAction.CallbackContext obj)
    {
        inventoryUI.InventoryInput();
    }

    void Update()
    {
        playerMovement.MovementSlideX(lateral.ReadValue<float>());
        playerMovement.MovementSlideZ(forwardBackward.ReadValue<float>());
        mouseLook.SetMouseX(look.ReadValue<Vector2>().x);
        mouseLook.SetMouseY(look.ReadValue<Vector2>().y);
    }

}
