using System;
using UnityEngine.InputSystem;

public interface IPlayerControl
{
    IInputActionCollection InputAction { get; }
    event Action<InputAction.CallbackContext> Move;
    event Action<InputAction.CallbackContext> Attack;
    void Enable();
    void OnMove(InputAction.CallbackContext context);
    void OnAttack(InputAction.CallbackContext context);
}