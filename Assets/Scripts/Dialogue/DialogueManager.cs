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

    private string continueText = "CONTINUE »";
    private string quitText = "EXIT";

    private Queue<string> sentences;
    private Animator dialogueAnimator;

    [Header("Question panel settings")]
    public TMP_Text questionText;
    public GameObject answerButtonPrefab;
    public GameObject questionPanel;

    private GameObject[] answerButtons;
    public Transform answersContainer;
    private Animator questionAnimator;

    private string answerwContainerName = "AnswersContainerl";

    private enum DialogType
    {
        None,
        Text,
        Question
    }
    private DialogType dialogType = DialogType.None;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueAnimator = dialoguePanel.GetComponent<Animator>();
        questionAnimator = questionPanel.GetComponent<Animator>();

        answerButtons = Array.Empty<GameObject>();
        //answersContainer = questionPanel.transform.Find(answerwContainerName);
    }

    private void Update()
    {
        if (!dialoguePanel.activeSelf && !questionPanel.activeSelf)
        {
            return;
        }

        switch (dialogType)
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
        dialoguePanel.SetActive(true);
        dialogueAnimator.SetBool("IsOpen", true);

        dialogType = DialogType.Text;
        speakerNameText.text = dialogue.speakerName;

        sentences.Clear();
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
        dialogueAnimator.SetBool("IsOpen", false);
        //dialoguePanel.SetActive(false);
    }

    public void StartQuestion(Question dialogue)
    {
        questionPanel.SetActive(true);
        questionAnimator.SetBool("IsOpen", true);

        questionText.text = dialogue.question;
        dialogType = DialogType.Question;

        if (dialogue.answers.Length > 4)
        {
            Debug.LogWarning("Many dialogue options. Consider reduce number of available options!");
        }

        foreach (var oldButton in answerButtons)
        {
            DestroyImmediate(oldButton);
        }

        answerButtons = new GameObject[dialogue.answers.Length];
        for (int i = 0; i < dialogue.answers.Length; i++)
        {
            string currentAnswer = dialogue.answers[i];
            answerButtons[i] = Instantiate(answerButtonPrefab, answersContainer, false);
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = $"{i + 1}. " + currentAnswer;
            answerButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    dialogue.pickedAnswer = currentAnswer;
                    Debug.Log("You clicked: " + currentAnswer);
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
        //questionPanel.SetActive(false);
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
