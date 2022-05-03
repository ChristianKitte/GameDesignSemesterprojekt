using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool InvertMouseY { get; private set; } = true;

    private InputActions _input;

    private void OnEnable()
    {
        _input = new InputActions();
        _input.Player.Enable();

        _input.Player.Move.performed += SetMove;
        _input.Player.Move.canceled += SetMove;

        _input.Player.Lock.performed += SetLook;
        _input.Player.Lock.canceled += SetLook;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= SetMove;
        _input.Player.Move.canceled -= SetMove;

        _input.Player.Lock.performed -= SetLook;
        _input.Player.Lock.canceled -= SetLook;

        _input.Player.Disable();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }
}