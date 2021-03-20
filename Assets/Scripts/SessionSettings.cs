using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Game Session Settings")]
public class SessionSettings : ScriptableObject
{
    public int Player1Character { get => p1Character; set => p1Character = value; }
    public int Player2Character { get => p2Character; set => p2Character = value; }

    [SerializeField]
    [Min(0)]
    private int p1Character = 0;

    [SerializeField]
    [Min(0)]
    private int p2Character = 1;
}