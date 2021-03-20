using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlMap;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, DefaultControl.IGameplayActions
{

    [SerializeField, Range(0,10)]
    private float movementSpeed = 5f;

    private DefaultControl inputs;
    private Vector2 movement = new Vector2();

    private void Awake() {
        inputs = new DefaultControl();
        inputs.Enable();
        inputs.Gameplay.SetCallbacks(this);
    }

    private void FixedUpdate() {
        var target = transform.position + new Vector3(movement.x, 0, movement.y);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
