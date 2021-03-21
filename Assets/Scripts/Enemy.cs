using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform track = null;

    [SerializeField]
    private float chaseSpeed = 2f;

    [SerializeField]
    private float attackRate = 2f;

    [SerializeField]
    private float damage = 3f;

    private EnemyVision vision;
    private PlayerController target;
    private Health targetHealth;
    private bool damaging = false;
    private CinemachineImpulseSource impulseSource;

    private void Awake() 
    {
        vision = GetComponentInChildren<EnemyVision>();
        TryGetComponent(out impulseSource);
        vision.TargetFound += OnTargetFound;
        vision.TargetLost += OnTargetLost;
        transform.position = track.position;
    }

    private void FixedUpdate() 
    {
        if (target==null)
        {
            transform.position = Vector3.MoveTowards(transform.position, track.position, Time.deltaTime * chaseSpeed);;
            return;
        }

        var targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * chaseSpeed);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (target != null && other.gameObject == target.gameObject)
        {
            damaging = true;
            StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (target == null || other.gameObject == target.gameObject)
        {
            damaging = false;
        }
    }

    IEnumerator DealDamage()
    {
        while(damaging)
        {
            targetHealth.Damage(damage);
            impulseSource.GenerateImpulse();
            yield return new WaitForSeconds(1/attackRate);
        }
    }

    private void OnTargetFound(PlayerController player)
    {
        target = player;
        target.TryGetComponent(out targetHealth);
    }

    private void OnTargetLost()
    {
        target = null;
        targetHealth = null;
    }
}
