using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Animator blackCanvasAnimator;

    public static bool cameraCanMove = true;
    public static bool gameIsPaused = false;
    private bool startDialogueEnded = false;
    internal static bool endedGame = false;

    public bool StartDialogueEnded
    {
        get { return startDialogueEnded; }
        set
        {
            if (value)
            {
                blackCanvasAnimator.SetBool("GameStarted", true);
                gameIsPaused = false;
            }

            startDialogueEnded = value;
        }
    }

    private void Start()
    {
        blackCanvasAnimator.gameObject.SetActive(true);
        GetComponent<DialogueTrigger>().TriggerDialogue();

        gameIsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !endedGame)
        {
            ChangePause();
        }

        if (startDialogueEnded)
        {
            if (blackCanvasAnimator.gameObject.activeSelf)
            {
                blackCanvasAnimator.gameObject.SetActive(false);
            }
        }
    }

    public void ChangePause()
    {
        if (!startDialogueEnded)
        {
            menuCanvas.gameObject.SetActive(!menuCanvas.gameObject.activeSelf);
            return;
        }

        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
        menuCanvas.gameObject.SetActive(gameIsPaused);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
