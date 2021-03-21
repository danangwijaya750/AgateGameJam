using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyVision : MonoBehaviour
{
    public event Action<PlayerController> TargetFound;
    public event Action TargetLost;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
            TargetFound?.Invoke(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            TargetLost?.Invoke();
        }
    }
}
