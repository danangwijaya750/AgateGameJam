using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shade : MonoBehaviour 
{

    [SerializeField]
    private PlayerController player = null;

    [SerializeField]
    private float delay = 1.5f;

    private Queue<Action> actions = new Queue<Action>();
    private Animator animator;
    private readonly int attackAnimId = Animator.StringToHash("attack");
    private readonly int movementAnimId = Animator.StringToHash("movement");
    private bool onDelay = true;

    IEnumerator Start()
    {
        animator = GetComponentInChildren<Animator>();
        player.Attack += OnAttack;
        player.Move += OnMove;
        yield return new WaitForSeconds(delay);
        onDelay = false;
    }

    private void OnAttack()
    {
        void attack()
        {
            animator.SetTrigger(attackAnimId);
        }
        actions.Enqueue(attack);
    }

    private void OnMove(Vector3 movement)
    {
        void move()
        {
            var target = transform.position + movement;
            var movementValue = Mathf.Abs(movement.normalized.x)+Mathf.Abs(movement.normalized.z);
            animator.SetFloat(movementAnimId, movementValue);
            if (Vector3.Distance(transform.position, target) > 0)
            {
                var direction = movement.normalized;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), player.TurnSpeed);
            }
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * player.MovementSpeed);
        }
        actions.Enqueue(move);
    }

    private void FixedUpdate() 
    {
        if (onDelay) return;
        if (actions.Count <= 0) return;
        actions.Dequeue()?.Invoke();
    }

}