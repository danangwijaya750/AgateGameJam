using System;
using UnityEngine.InputSystem;
using ControlMap;

public class Player1Control : IPlayerControl, Player1InputAction.IGameplayActions
{
    public IInputActionCollection InputAction => inputAction;
    Player1InputAction inputAction;
    
    public Player1Control()
    {
        inputAction = new Player1InputAction();
        inputAction.Gameplay.SetCallbacks(this);
    }

    public event Action<InputAction.CallbackContext> Move;
    public event Action<InputAction.CallbackContext> Attack;

    public void Enable()
    {
        inputAction.Enable();
    }

    public void Disable()
    {
        inputAction.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Attack?.Invoke(context);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move?.Invoke(context);
    }
}