﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlMap;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, DefaultControl.IGameplayActions
{

    [SerializeField, Range(0,10)]
    private float movementSpeed = 5f;

    [SerializeField, Range(5, 90)]
    private float turnSpeed = 15f;

    [SerializeField]
    private PlayerAnimation animEvent = null;

    private DefaultControl inputs;
    private Vector2 movementInput = new Vector2();
    private Animator animator = null;
    private readonly int movementAnimId = Animator.StringToHash("movement");
    private readonly int attackAnimId = Animator.StringToHash("attack");
    private bool movementEnabled = true;

    private void Awake() {
        transform.GetChild(0).TryGetComponent(out animator);
        inputs = new DefaultControl();
        inputs.Enable();
        inputs.Gameplay.SetCallbacks(this);
        animEvent.OnAttackStart += OnAttackStart;
        animEvent.OnAttackEnd += OnAttackEnd;
    }

    private void OnAttackStart()
    {
        movementEnabled = false;
    }
    
    private void OnAttackEnd()
    {
        movementEnabled = true;
    }

    private void FixedUpdate() {
        if (!movementEnabled) return;

        var playerMovement = new Vector3(movementInput.x, 0, movementInput.y);
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
            animator.SetTrigger(attackAnimId);
    }
}
