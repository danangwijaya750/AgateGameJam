using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Game Session Settings")]
public class SessionSettings : ScriptableObject
{
    public int GameMode { get => gameMode; set => gameMode = value; }

    [SerializeField]
    [Min(0)]
    private int gameMode = 0;

}