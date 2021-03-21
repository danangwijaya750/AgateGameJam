using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private Slider healthBar = null;

    [SerializeField]
    private float animationSpeed = 0.5f;

    private float currentHealth = 100f;

    private void Awake() 
    {
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        var targetHealth = Mathf.Max(currentHealth, 0);
        StartCoroutine(AnimateHealthChange(targetHealth));
        if (currentHealth <= 0)
        {
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
