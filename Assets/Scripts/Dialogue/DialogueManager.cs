using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Text dialogue settings")]
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public TMP_Text dialogueButtonText;
    public GameObject dialoguePanel;

    private string continueText = "DALEJ »";
    private string quitText = "ZAKOÑCZ";

    private bool isStartDialogue = false;
    public static bool startDialogueEnded = false;
    private bool openPanel = false;

    private Queue<string> sentences;
    [SerializeField]
    private Animator dialogueAnimator;

    [Header("Question panel settings")]
    public TMP_Text questionText;
    public GameObject answerButtonPrefab;
    public GameObject questionPanel;

    private GameObject[] answerButtons;
    public Transform answersContainer;
    [SerializeField]
    private Animator questionAnimator;

    private enum DialogType
    {
        None,
        Text,
        Question
    }
    private DialogType dialogueType = DialogType.None;

    private void Update()
    {
        if (!dialoguePanel.activeSelf && !questionPanel.activeSelf)
        {
            return;
        }

        switch (dialogueType)
        {
            case DialogType.None:
                break;

            case DialogType.Text:
                DialogueKeyboardControls();
                break;

            case DialogType.Question:
                QuestionKeyboardControls();
                break;

            default:
                break;
        }
    }

    private void DialogueKeyboardControls()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(dialoguePanel);
        DisplayNextSentence();
    }
    private void QuestionKeyboardControls()
    {
        InvokeAnswerButton(KeyCode.Alpha1);
        InvokeAnswerButton(KeyCode.Alpha2);
        InvokeAnswerButton(KeyCode.Alpha3);
        InvokeAnswerButton(KeyCode.Alpha4);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        if (openPanel || dialogue == null)
            return;

        openPanel = true;
        ShowPanelImage(dialoguePanel.GetComponent<Image>());
        dialoguePanel.SetActive(true);
        GameManager.cameraCanMove = false;
        dialogueAnimator.SetBool("IsOpen", true);

        dialogueType = DialogType.Text;
        if (dialogue.speakerName == "Narrator" && !isStartDialogue)
        {
            isStartDialogue = true;
        }
        else
        {
            isStartDialogue = false;
            speakerNameText.text = dialogue.speakerName;
        }

        sentences = new Queue<string>();
        Array.ForEach(dialogue.sentences, s => sentences.Enqueue(s));
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //dialogueText.text = string.Empty;
        string sentence = sentences.Dequeue();
        dialogueButtonText.text = sentences.Count == 0 ? quitText : continueText;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    private void EndDialogue()
    {
        openPanel = false;
        dialogueAnimator.SetBool("IsOpen", false);
        GameManager.cameraCanMove = true;

        if (isStartDialogue)
            FindObjectOfType<GameManager>().StartDialogueEnded = true;

        StartCoroutine(HidePanelImage(dialoguePanel.GetComponent<Image>()));
    }

    public void StartQuestion(Question question)
    {
        if (openPanel)
            return;

        openPanel = true;
        ShowPanelImage(questionPanel.GetComponent<Image>());
        questionPanel.SetActive(true);
        questionAnimator.SetBool("IsOpen", true);
        GameManager.cameraCanMove = false;

        questionText.text = question.question;
        dialogueType = DialogType.Question;

        if (question.answers.Length > 4)
        {
            Debug.LogWarning("Many dialogue options. Consider reduce number of available options!");
        }

        if (answerButtons == null)
        {
            answerButtons = new GameObject[question.answers.Length];
        }
        else
        {
            foreach (var oldButton in answerButtons)
            {
                DestroyImmediate(oldButton);
            }
        }

        answerButtons = new GameObject[question.answers.Length];
        for (int i = 0; i < question.answers.Length; i++)
        {
            string currentAnswer = question.answers[i];
            answerButtons[i] = Instantiate(answerButtonPrefab, answersContainer, false);
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = $"{i + 1}. " + currentAnswer;
            answerButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    question.ValidateAnswer(currentAnswer);
                    EndQuestion();
                });
        }
    }

    private void InvokeAnswerButton(KeyCode keyCode)
    {
        if (!Input.GetKeyDown(keyCode))
        {
            return;
        }

        int buttonIndex = keyCode - KeyCode.Alpha1;
        if (answerButtons.Length <= buttonIndex)
        {
            return;
        }

        answerButtons[buttonIndex].GetComponent<Button>().onClick.Invoke();
        return;
    }

    private void EndQuestion()
    {
        questionAnimator.SetBool("IsOpen", false);
        foreach (var oldButton in answerButtons)
        {
            DestroyImmediate(oldButton);
        }
        GameManager.cameraCanMove = true;
        openPanel = false;
        StartCoroutine(HidePanelImage(questionPanel.GetComponent<Image>()));
    }


    private IEnumerator HidePanelImage(Image image)
    {
        yield return new WaitForSeconds(0.5f);

        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    private void ShowPanelImage(Image image)
    {
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
