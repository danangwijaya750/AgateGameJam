using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleOnActiveEvent : MonoBehaviour
{
    public UnityEvent OnToggleActivated = null;

    private Toggle attachedToggle = null;

    private void Awake()
    {
        TryGetComponent(out attachedToggle);
        attachedToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool active)
    {
        if (active)
        {
            OnToggleActivated?.Invoke();
        }
    }
}