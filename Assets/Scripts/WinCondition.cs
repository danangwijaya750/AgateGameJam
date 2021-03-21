using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    private List<Health> enemies = new List<Health>();
    
    [SerializeField]
    private GameObject winPanel = null;
    
    [SerializeField]
    private GameObject losePanel = null;

    [SerializeField]
    private Health target = null;

    private void Awake() 
    {
        target.Die += OnLose;
        foreach (var enemy in enemies)
        {
            enemy.Die += () => OnDie(enemy);
        }
    }

    private void FixedUpdate() 
    {
        if (target == null) return;
        if (enemies.Count > 0) return;
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 1) return;

        winPanel.SetActive(true);
        if (target.TryGetComponent(out PlayerController control))
        {
            control.Inputs.Disable();
        }
    }

    private void OnDie(Health enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnLose()
    {
        losePanel.SetActive(true);
        if (target.TryGetComponent(out PlayerController control))
        {
            control.Inputs.Disable();
        }
    }
}
