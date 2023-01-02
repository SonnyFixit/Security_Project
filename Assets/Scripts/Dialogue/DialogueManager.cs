using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public TMP_Text dialogueButtonText;

    public Animator dialogueBoxAnimator;

    public GameObject dialogueBox;

    private string continueText = "CONTINUE »";
    private string quitText = "EXIT";

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (!dialogueBox.active)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(dialogueBox);
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        speakerNameText.text = dialogue.speakerName;
        dialogueBoxAnimator.SetBool("IsOpen", true);
        sentences.Clear();

        Array.ForEach(dialogue.sentences, s => sentences.Enqueue(s));

        //foreach (string sentence in dialogue.sentences)
        //{
        //    sentences.Enqueue(sentence);
        //}

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
}
