using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    private List<Health> enemies = new List<Health>();
    
    [SerializeField]
    private GameObject winPanel = null;

    private Transform target = null;

    private void Awake() 
    {
        foreach (var enemy in enemies)
        {
            enemy.Die += () => OnDie(enemy);
        }
    }

    private void FixedUpdate() 
    {
        if (target == null) return;
        if (enemies.Count > 0) return;
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 1) return;

        winPanel.SetActive(true);
        if (target.TryGetComponent(out PlayerController control))
        {
            control.Inputs.Disable();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void OnDie(Health enemy)
    {
        enemies.Remove(enemy);
    }
}
