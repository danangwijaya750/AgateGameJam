using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Health : MonoBehaviour
{
    public float CurrentHealth => currentHealth;

    public event Action Die;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private GameObject explosion = null;

    [SerializeField]
    private Transform explosionSpawnPoint = null;

    [SerializeField]
    private float explosionForce = 1f;

    [SerializeField]
    private Slider healthBar = null;

    [SerializeField]
    private float animationSpeed = 0.5f;

    private float currentHealth = 100f;
    private CinemachineImpulseSource impulseSource;

    private void Awake() 
    {
        currentHealth = maxHealth;
        TryGetComponent(out impulseSource);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        var targetHealth = Mathf.Max(currentHealth, 0);
        StartCoroutine(AnimateHealthChange(targetHealth));
        if (currentHealth <= 0)
        {
            Destroy(Instantiate(explosion, explosionSpawnPoint.position, Quaternion.identity), 2);
            impulseSource.GenerateImpulse(explosionForce);
            Die?.Invoke();
            Destroy(gameObject);
        }
    }

    IEnumerator AnimateHealthChange(float target)
    {
        float t = 0;
        do
        {
            healthBar.value = Mathf.Lerp(healthBar.value, target/maxHealth, t);
            t += Time.deltaTime / animationSpeed;
            yield return null;
        }
        while (t <= 1);
    }
}
