using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CombatMusicActivator : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot music = null;

    [SerializeField]
    private AudioMixerSnapshot combat = null;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            combat.TransitionTo(1f);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            music.TransitionTo(1f);
        }
    }
}
