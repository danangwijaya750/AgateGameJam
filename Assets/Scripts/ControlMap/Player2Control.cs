using System;
using UnityEngine.InputSystem;
using ControlMap;

public class Player2Control : IPlayerControl, Player2InputAction.IGameplayActions
{
    public IInputActionCollection InputAction => inputAction;
    Player2InputAction inputAction;
    
    public Player2Control()
    {
        inputAction = new Player2InputAction();
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