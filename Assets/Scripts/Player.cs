﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlMap;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, DefaultControl.IGameplayActions
{

    [SerializeField, Range(0,10)]
    private float movementSpeed = 5f;

    [SerializeField, Range(5, 90)]
    private float turnSpeed = 15f;

    private DefaultControl inputs;
    private Vector2 movementInput = new Vector2();

    private void Awake() {
        inputs = new DefaultControl();
        inputs.Enable();
        inputs.Gameplay.SetCallbacks(this);
    }

    private void FixedUpdate() {
        var playerMovement = new Vector3(movementInput.x, 0, movementInput.y);
        var target = transform.position + playerMovement;
        if (playerMovement != Vector3.zero)
        {
            var direction = playerMovement.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}