using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuelListener : MonoBehaviour
{
    [SerializeField]
    private List<Health> players = null;

    [SerializeField]
    private GameObject winPanel = null;

    [SerializeField]
    private TextMeshProUGUI winText = null;

    private void Awake() 
    {
        var count = players.Count;
        for (int i = 0; i < count; i++)
        {
            Health health = players[i];
            health.Die += () => OnPlayerDie(health);
        }    
    }

    private void OnPlayerDie(Health health)
    {
        players.Remove(health);
        winPanel.SetActive(true);
        winText.text = $"{players[0].name} wins!";
        players[0].GetComponent<PlayerController>().Inputs.Disable();
    }
}
