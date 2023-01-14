using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue data")]
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public TMP_Text dialogueButtonText;
    public GameObject dialogueBox;

    private string continueText = "CONTINUE »";
    private string quitText = "EXIT";

    private Queue<string> sentences;
    private Animator dialogueBoxAnimator;

    [Header("Choose option data")]
    public TMP_Text questionText;
    public GameObject optionButtonPrefab;
    public GameObject optionDialogueBox;

    private GameObject[] optionsButtons;
    private Transform optionsContainer;
    private Animator optionDialogueBoxAnimator;

    private enum DialogType
    {
        None,
        Text,
        Option
    }
    private DialogType dialogType = DialogType.None;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueBoxAnimator = dialogueBox.GetComponent<Animator>();
        optionDialogueBoxAnimator = optionDialogueBox.GetComponent<Animator>();

        optionsButtons = Array.Empty<GameObject>();
        optionsContainer = optionDialogueBox.transform.Find("Options");
    }

    private void Update()
    {
        if (!dialogueBox.active && !optionDialogueBox.active)
        {
            return;
        }

        switch (dialogType)
        {
            case DialogType.None:
                break;

            case DialogType.Text:
                TextDialogueKeyboardControls();
                break;

            case DialogType.Option:
                OptionDialogueKeyboardControls();
                break;

            default:
                break;
        }
    }

    private void TextDialogueKeyboardControls()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(dialogueBox);
        DisplayNextSentence();
    }

    private void InvokeOptionButton(KeyCode keyCode)
    {
        if (!Input.GetKeyDown(keyCode))
        {
            return;
        }

        int buttonIndex = keyCode - KeyCode.Alpha1;
        if (optionsButtons.Length <= buttonIndex)
        {
            return;
        }

        optionsButtons[buttonIndex].GetComponent<Button>().onClick.Invoke();
        return;
    }

    private void OptionDialogueKeyboardControls()
    {
        InvokeOptionButton(KeyCode.Alpha1);
        InvokeOptionButton(KeyCode.Alpha2);
        InvokeOptionButton(KeyCode.Alpha3);
        InvokeOptionButton(KeyCode.Alpha4);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueBoxAnimator.SetBool("IsOpen", true);

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

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);
    }

    public void StartOptionsDialogue(OptionDialogue dialogue)
    {
        optionDialogueBox.SetActive(true);
        questionText.text = dialogue.question;
        dialogType = DialogType.Option;

        if (dialogue.options.Length > 4)
        {
            Debug.LogWarning("Many dialogue options. Consider reduce number of available options!");
        }

        foreach (var oldButton in optionsButtons)
        {
            DestroyImmediate(oldButton);
        }

        optionsButtons = new GameObject[dialogue.options.Length];
        for (int i = 0; i < dialogue.options.Length; i++)
        {
            string currentOption = dialogue.options[i];
            optionsButtons[i] = Instantiate(optionButtonPrefab, optionsContainer, false);
            optionsButtons[i].GetComponentInChildren<TMP_Text>().text = $"{i + 1}. " + currentOption;
            optionsButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    dialogue.chosenOption = currentOption;
                    Debug.Log("You clicked: " + currentOption);
                    EndOptionDialogue();
                });
        }
    }

    private void EndOptionDialogue()
    {
        //dialogueBoxAnimator.SetBool("IsOpen", false);
        optionDialogueBox.SetActive(false);
    }
}
