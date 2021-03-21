using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public event Action<Health> Hit;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out Health health))
        {
            Hit?.Invoke(health);
        }
    }
}
