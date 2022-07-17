using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

// Handles UI for Dialogue and notifies ProgressManager when Dialogue is done
public class DialogueManager : MonoBehaviour {
    [SerializeField]
    [TextArea(2, 10)]
    private string[] sentences;

    private bool isTyping;
    private Queue<string> dialogue;
    
    private ProgressManager progressManager;

    [SerializeField] private UnityEvent OnDialogueEnd;

    #region UI Vars
    [SerializeField] private float typeWaitTime;
    private TMP_Text dialogueText;

    private string curSentence;
    #endregion

    private void Start() {
        dialogueText = GameObject.Find("DialogueBox").GetComponent<TMP_Text>();
        progressManager = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();

        dialogue = new Queue<string>();
        StartDialogue();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            if (isTyping) {
                StopAllCoroutines();
                EndTyping();
                dialogueText.text = curSentence;
            } 
            else DisplayNextSentence();
    }

    private void StartDialogue() {
        dialogue.Clear();

        foreach (string sentence in sentences)
            dialogue.Enqueue(sentence);

        dialogueText.text = "";
        isTyping = false;
    }

    private void DisplayNextSentence() {
        if (dialogue.Count == 0) return;

        string sentence = dialogue.Dequeue();
        curSentence = sentence;

        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence));
    }

    private IEnumerator CoTypeSentence(string sentence) {
        isTyping = true;
        
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeWaitTime);
        }

        EndTyping();
    }

    private void EndTyping() {
        isTyping = false;

        if (dialogue.Count == 0)
            StartCoroutine(CoEndDialogue());
    }

    private IEnumerator CoEndDialogue() {
        yield return null;

        OnDialogueEnd?.Invoke();

        if (SceneManager.GetActiveScene().name == "5_PetruDecision" ||
             SceneManager.GetActiveScene().name == "4_Elder")
            yield break;

        progressManager.SetCanProgress(true);
    }
}
