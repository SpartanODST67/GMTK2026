using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    InputAction altAttackAction;

    [SerializeField] InputPuppet puppet;

    void Awake()
    {
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        attackAction = input.actions["Attack"];
        altAttackAction = input.actions["Alt Attack"];
    }

    void OnEnable()
    {
        jumpAction.started += JumpInput;
        attackAction.started += AttackInput;
        altAttackAction.started += AltAttackInput;
    }

    void OnDisable()
    {
        jumpAction.started -= JumpInput;
        attackAction.started -= AttackInput;
        altAttackAction.started -= AltAttackInput;
    }

    void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        puppet.MoveAction(movementVector);
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        puppet.JumpAction();        
    }

    private void AttackInput(InputAction.CallbackContext context)
    {
        puppet.AttackAction();
    }

    private void AltAttackInput(InputAction.CallbackContext context)
    {
        puppet.AltAttackAction();
    }
}
