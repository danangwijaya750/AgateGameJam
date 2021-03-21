using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class DuelListener : MonoBehaviour
{
    [SerializeField]
    private List<Health> players = null;

    [SerializeField]
    private GameObject winPanel = null;

    [SerializeField]
    private TextMeshProUGUI winText = null;

    [SerializeField]
    private AudioSource winSound = null;

    [SerializeField]
    private AudioMixer mixer = null;

    private void Awake() 
    {
        var count = players.Count;
        for (int i = 0; i < count; i++)
        {
            Health health = players[i];
            health.Die += () => OnPlayerDie(health);
        }
        mixer.FindSnapshot("Main").TransitionTo(0.5f);
    }

    private void OnPlayerDie(Health health)
    {
        var snapshot = mixer.FindSnapshot("SoundEffect");
        snapshot.TransitionTo(0.5f);
        winSound.Play();
        players.Remove(health);
        winPanel.SetActive(true);
        winText.text = $"{players[0].name} wins!";
        players[0].GetComponent<PlayerController>().Inputs.Disable();
    }
}
