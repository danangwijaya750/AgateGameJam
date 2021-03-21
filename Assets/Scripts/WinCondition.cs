using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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

    [SerializeField]
    private AudioMixerSnapshot seSnapshot = null;

    [SerializeField]
    private AudioMixerSnapshot mainSnapshot = null;

    [SerializeField]
    private AudioClip winClip = null, loseClip = null;

    private AudioSource audioSource;
    private bool win = false;

    private void Awake() 
    {
        mainSnapshot.TransitionTo(0.5f);
        TryGetComponent(out audioSource);
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
        if (win) return;

        seSnapshot.TransitionTo(0.5f);
        audioSource.PlayOneShot(winClip);
        winPanel.SetActive(true);
        if (target.TryGetComponent(out PlayerController control))
        {
            control.Inputs.Disable();
        }
        win = true;
    }

    private void OnDie(Health enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnLose()
    {
        seSnapshot.TransitionTo(0.5f);
        audioSource.PlayOneShot(loseClip);
        losePanel.SetActive(true);
        if (target.TryGetComponent(out PlayerController control))
        {
            control.Inputs.Disable();
        }
    }
}
