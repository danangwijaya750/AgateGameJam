using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    private RectTransform snackbarPrefab = null;

    [SerializeField]
    private Transform snackbarSlot = null;

    [SerializeField]
    private RectTransform canvas = null;

    private EnemyVision vision;
    private PlayerController target;
    private Health targetHealth;
    private bool damaging = false;
    private CinemachineImpulseSource impulseSource;
    private RectTransform snackbar = null;
    private Vector2 uiOffset = Vector2.zero;
    private Health health;

    private void Awake()
    {
        vision = GetComponentInChildren<EnemyVision>();
        uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);
        snackbar = Instantiate(snackbarPrefab, canvas);
        CalculateSnackbarPosition();
        TryGetComponent(out impulseSource);
        TryGetComponent(out health);
        health.Die += () => Destroy(snackbar.gameObject);
        health.HealthBar = snackbar.GetComponent<Slider>();
        vision.TargetFound += OnTargetFound;
        vision.TargetLost += OnTargetLost;
        transform.position = track.position;
    }

    private void CalculateSnackbarPosition()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(snackbarSlot.position);
        Vector2 proportionalPosition = new Vector2(viewportPosition.x * canvas.sizeDelta.x, viewportPosition.y * canvas.sizeDelta.y);
        snackbar.localPosition = proportionalPosition - uiOffset;
    }

    private void FixedUpdate() 
    {
        CalculateSnackbarPosition();
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
            // impulseSource.GenerateImpulse();
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
