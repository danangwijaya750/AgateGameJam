using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlMap;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed => movementSpeed;
    public float TurnSpeed => turnSpeed;

    public event Action<Vector3> Move;
    public event Action<bool> Attack;
    
    [SerializeField, Range(0,10)]
    private float movementSpeed = 5f;

    [SerializeField, Range(5, 90)]
    private float turnSpeed = 15f;

    [SerializeField]
    private PlayerAnimation animEvent = null;

    [SerializeField]
    private PlayerEnum playerNumber = PlayerEnum.Player1;

    private IPlayerControl inputs;
    private Vector2 movementInput = new Vector2();
    private Animator animator = null;
    private readonly int movementAnimId = Animator.StringToHash("movement");
    private readonly int attackAnimId = Animator.StringToHash("attack");
    private bool attacking = false;
    private bool attackInput = false;

    private void Awake() 
    {
        transform.GetChild(0).TryGetComponent(out animator);
        switch (playerNumber)
        {
            case PlayerEnum.Player1:
                inputs = new Player1Control();
                if (Gamepad.all.Count > 0)
                {
                    var user = InputUser.PerformPairingWithDevice(Gamepad.all[0]);
                    user.AssociateActionsWithUser(inputs.InputAction);
                    user.ActivateControlScheme("Gamepad");
                }
                break;
            case PlayerEnum.Player2:
                inputs = new Player2Control();
                if (Gamepad.all.Count > 1)
                {
                    var user = InputUser.PerformPairingWithDevice(Gamepad.all[1]);
                    user.AssociateActionsWithUser(inputs.InputAction);
                    user.ActivateControlScheme("Gamepad");
                }
                break;
            default:
                break;
        }
        inputs.Enable();
        inputs.Move += OnMove;
        inputs.Attack += OnAttack;
        animEvent.OnAttackStart += OnAttackStart;
        animEvent.OnAttackEnd += OnAttackEnd;
    }

    private void OnAttackStart()
    {
        attacking = true;
    }
    
    private void OnAttackEnd()
    {
        attacking = false;
    }

    private void FixedUpdate() 
    {
        Attack?.Invoke(attackInput);
        if (attackInput)
        {
            animator.SetTrigger(attackAnimId);
            attackInput = false;
        }

        if (attacking) 
        {
            Move?.Invoke(Vector3.zero);
            return;
        }

        var playerMovement = new Vector3(movementInput.x, 0, movementInput.y);
        Move?.Invoke(playerMovement);
        var target = transform.position + playerMovement;
        var movementValue = Mathf.Abs(playerMovement.normalized.x)+Mathf.Abs(playerMovement.normalized.z);
        animator.SetFloat(movementAnimId, movementValue);
        if (Vector3.Distance(transform.position, target) > 0)
        {
            var direction = playerMovement.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);


    }

    private void Reset() {
        animEvent = GetComponentInChildren<PlayerAnimation>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            attackInput = true;
        }
    }
}
