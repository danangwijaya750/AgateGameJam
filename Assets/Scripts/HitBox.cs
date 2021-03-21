using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public event Action Hit;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Hit?.Invoke();
        }
    }
}
