using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MinigameTrigger : MonoBehaviour
{
    public MinigameHackData hackData;
    public void TriggerMinigame()
    {
        FindObjectOfType<MinigameHackManager>().StartGame(hackData);
    }
}
