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

    [SerializeField]
    private HitBox hitBox = null;

    [SerializeField]
    private float distanceToDamageRatio = 1f;

    [SerializeField]
    private GameObject sword = null;

    private Queue<Action> attackActions = new Queue<Action>();
    private Queue<Action> movementActions = new Queue<Action>();
    private Animator animator;
    private PlayerAnimation animEvent;
    private MeshRenderer swordRenderer;
    private readonly int attackAnimId = Animator.StringToHash("attack");
    private readonly int movementAnimId = Animator.StringToHash("movement");
    private bool onDelay = true;
    private Color baseColor;

    IEnumerator Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.TryGetComponent(out animEvent);
        sword.TryGetComponent(out swordRenderer);
        player.TryGetComponent(out Health health);
        health.Die += OnDie;
        baseColor = swordRenderer.materials[1].color;
        player.Attack += OnAttack;
        player.Move += OnMove;
        animEvent.OnSlashStart += OnSlashStart;
        animEvent.OnSlashEnd += OnSlashEnd;
        hitBox.Hit += OnHit;
        yield return new WaitForSeconds(delay);
        onDelay = false;
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }

    private void OnAttack(bool performed)
    {
        if (!performed)
        {
            void doNothing() {}
            attackActions.Enqueue(doNothing);
            return;
        }

        void attack()
        {
            animator.SetTrigger(attackAnimId);
        }
        attackActions.Enqueue(attack);
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
        movementActions.Enqueue(move);
    }

    private void OnSlashStart()
    {
        hitBox.gameObject.SetActive(true);
    }

    private void OnSlashEnd()
    {
        hitBox.gameObject.SetActive(false);
    }

    private void OnHit(Health health)
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        var damageAmount = distance * distanceToDamageRatio;
        if (health.gameObject == player.gameObject) damageAmount *= 2;
        health.Damage(damageAmount);
    }

    private void FixedUpdate() 
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        swordRenderer.materials[1].SetColor("_EmissionColor", baseColor * distanceToPlayer);

        if (onDelay) return;
        if (attackActions.Count <= 0) return;
        if (movementActions.Count <= 0) return;
        attackActions.Dequeue()?.Invoke();
        movementActions.Dequeue()?.Invoke();
    }

}