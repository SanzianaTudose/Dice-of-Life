using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Handles UI for Dialogue and notifies ProgressManager when Dialogue is done
public class DialogueManager : MonoBehaviour {
    [SerializeField]
    [TextArea(2, 10)]
    private string[] sentences;

    private Queue<string> dialogue;
    
    private ProgressManager progressManager;

    #region UI Vars
    [SerializeField] private float typeWaitTime;
    private TMP_Text dialogueText;
    #endregion

    private void Start() {
        dialogueText = GameObject.Find("DialogueBox").GetComponent<TMP_Text>();
        progressManager = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();

        dialogue = new Queue<string>();
        StartDialogue();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextSentence();
        }
    }

    private void StartDialogue() {
        dialogue.Clear();

        foreach (string sentence in sentences)
            dialogue.Enqueue(sentence);

        dialogueText.text = "";
    }

    private void DisplayNextSentence() {
        if (dialogue.Count == 0) return;

        string sentence = dialogue.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence));
    }

    private IEnumerator CoTypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeWaitTime);
        }

        if (dialogue.Count == 0)
            EndDialogue();
    }

    private void EndDialogue() {
        progressManager.SetCanProgress(true);
    }
}
