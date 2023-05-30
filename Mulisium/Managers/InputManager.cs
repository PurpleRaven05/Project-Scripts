using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //
    Input playerInputActions;
    Input.CharacterMovementActions characterMovementMap;
    Input.UIActions uiMap;
    //
    [HideInInspector] public Vector2 moveDirectionVector;
    [HideInInspector] public Vector2 mouseDirectionVector;
    [HideInInspector] public bool jumping;
    [HideInInspector] public bool running;
    [HideInInspector] public bool crouching;
    [HideInInspector] public bool interacting;
    [HideInInspector] public bool gamePaused;

    void Awake()
    {
        playerInputActions = new Input();

        characterMovementMap = playerInputActions.CharacterMovement;
        uiMap = playerInputActions.UI;

        characterMovementMap.Enable();
        uiMap.Enable();

        //Action maps methods
        //
        characterMovementMap.MouseDrag.started += MouseMovement;
        characterMovementMap.MouseDrag.performed += MouseMovement;
        characterMovementMap.MouseDrag.canceled += MouseMovement;
        //
        characterMovementMap.Movement.started += MovementDirection;
        characterMovementMap.Movement.performed += MovementDirection;
        characterMovementMap.Movement.canceled += MovementDirection;
        //
        characterMovementMap.Jump.started += Jump;
        characterMovementMap.Jump.canceled += Jump;
        //
        characterMovementMap.Run.started += Run;
        characterMovementMap.Run.canceled += Run;
        //
        characterMovementMap.Crouch.started += Crouch;
        characterMovementMap.Crouch.canceled += Crouch;
        //
        characterMovementMap.Interact.started += Interact;
        //characterMovementMap.Interact.canceled += Interact;
        //
        uiMap.Pause.started += PauseGame;

        //Events
        EventManager.OnEnablePlayerMovement += EnablePlayerMovementMap;
        EventManager.OnDisablePlayerMovement += DisablePlayerMovementMap;
    }
    #region characterMovementMap methods
    void MouseMovement(InputAction.CallbackContext ctx)
    {
        mouseDirectionVector = ctx.ReadValue<Vector2>();
        //Debug.Log("Mouse Direction: " + mouseDirectionVector);
    }
    void MovementDirection(InputAction.CallbackContext ctx)
    {
        moveDirectionVector = ctx.ReadValue<Vector2>();
        //Debug.Log("Move Direction: " + moveDirection);
    }
    void Jump(InputAction.CallbackContext ctx)
    {
        jumping = ctx.ReadValueAsButton();
        //Debug.Log("Jumping: " + jumping);
    }
    void Run(InputAction.CallbackContext ctx)
    {
        running = ctx.ReadValueAsButton();
        //Debug.Log("Running: " + running);
    }
    void Crouch(InputAction.CallbackContext ctx)
    {
        crouching = ctx.ReadValueAsButton();
        //Debug.Log("Crouching: " + crouching);
    }
    void Interact(InputAction.CallbackContext ctx)
    {
        interacting = ctx.ReadValueAsButton();
        EventManager.OnInteractPressed?.Invoke();
        //Debug.Log("Interacting: " + interacting);
    }
    #endregion

    #region uiMap methods
    void PauseGame(InputAction.CallbackContext ctx)
    {
        gamePaused = ctx.ReadValueAsButton();
        EventManager.OnChangePauseMenu?.Invoke();
        //Debug.Log("Interacting: " + interacting);
    }
    #endregion

    #region common methods
    void DisablePlayerMovementMap()
    {
        characterMovementMap.Disable();
    }
    void EnablePlayerMovementMap()
    {
        characterMovementMap.Enable();
    }
    #endregion
    void OnDisable()
    {
        playerInputActions.Disable();

        //Action maps methods
        //
        characterMovementMap.MouseDrag.started -= MouseMovement;
        characterMovementMap.MouseDrag.performed -= MouseMovement;
        characterMovementMap.MouseDrag.canceled -= MouseMovement;
        //
        characterMovementMap.Movement.started -= MovementDirection;
        characterMovementMap.Movement.performed -= MovementDirection;
        characterMovementMap.Movement.canceled -= MovementDirection;
        //
        characterMovementMap.Jump.started -= Jump;
        characterMovementMap.Jump.canceled -= Jump;
        //
        characterMovementMap.Run.started -= Run;
        characterMovementMap.Run.canceled -= Run;
        //
        characterMovementMap.Crouch.started -= Crouch;
        characterMovementMap.Crouch.canceled -= Crouch;
        //
        characterMovementMap.Interact.started -= Interact;
        //characterMovementMap.Interact.canceled -= Interact;
        //
        uiMap.Pause.started -= PauseGame;

        //Events
        EventManager.OnEnablePlayerMovement -= EnablePlayerMovementMap;
        EventManager.OnDisablePlayerMovement -= DisablePlayerMovementMap;
    }
}
