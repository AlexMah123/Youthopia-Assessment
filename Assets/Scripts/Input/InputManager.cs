using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;

    public static Action OnInteract;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        //setup inputActions
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];

        //bind listeners
        interactAction.performed += HandleInteractAction;
    }

    private void Update()
    {
        if (TimeManager.isTimePaused) return;

        Movement = moveAction.ReadValue<Vector2>();
    }

    public void HandleInteractAction(InputAction.CallbackContext context)
    {
        if (TimeManager.isTimePaused) return;

        if (context.action.IsPressed())
        {
            OnInteract?.Invoke();
        }
    }


}
