using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] InputPuppet puppet;

    void Awake()
    {
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
    }

    void OnEnable()
    {
        jumpAction.started += JumpInput;
    }

    void OnDisable()
    {
        jumpAction.started -= JumpInput;
    }

    void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        if(movementVector.magnitude > 0)
            puppet.MoveAction(movementVector);
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        puppet.JumpAction();        
    }
}
