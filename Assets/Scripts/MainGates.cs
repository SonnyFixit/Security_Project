using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGates : MonoBehaviour
{
    public bool firstLightUnlock;
    public bool secondLightUnlock;
    public bool turnedOffInteractable = false;
    private DialogueTrigger dialogueTrigger;

    public GameObject endGameCanvas;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public virtual void OnMouseDown()
    {
        if (!firstLightUnlock || !secondLightUnlock)
        {
            if (!turnedOffInteractable)
                dialogueTrigger.TriggerDialogue();

            return;
        }

        EndGame();
    }

    private void EndGame()
    {
        endGameCanvas.gameObject.SetActive(true);
        GameManager.endedGame = true;
        GameManager.cameraCanMove = false;
        GameManager.gameIsPaused = true;
    }

    private void Update()
    {
        if (turnedOffInteractable)
            return;

        if (firstLightUnlock && secondLightUnlock)
        {
            dialogueTrigger.enabled = false;
            //GetComponent<Collider2D>().enabled = false;
            turnedOffInteractable = true;
        }
    }
}
