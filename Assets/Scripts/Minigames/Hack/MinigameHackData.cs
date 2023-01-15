using System;
using UnityEngine;

[Serializable]
public class MinigameHackData
{
    [Header("Max 6")]
    public string[] targets;
    public string[] availableHacks;
    public float timeLeft;
}