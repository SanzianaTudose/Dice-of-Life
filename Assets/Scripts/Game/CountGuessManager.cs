using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Base class for all Count Guess games in different scenes
// FOR NOW: Handles everything! Logic, UI, Sound & Input
public class CountGuessManager : MonoBehaviour {

    [SerializeField] protected float rhythm;
    [SerializeField] protected int[] numbers;

    [Header("Dependencies")]
    #region UI Vars
    [SerializeField] protected TMP_Text numberText;
    #endregion

    #region Sound Vars
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip rythmClickClip;
    #endregion

    #region Logic Vars
    // Game state vars
    private bool gameStarted = false;
    protected bool gameOver = false;
    protected bool isFirstRound = true;

    protected int curNumberInd;
    #endregion

    private void Awake() {
        DisplayEmpty();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update() {
        // Handle player input
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameStarted) {
                StartGame();
                return;
            }
            
            OnNumberChosen();
        }
    }

    private void StartGame() {
        gameStarted = true;

        curNumberInd = -1;
        StartCoroutine(CoRhythmCountdown());  
    }

    protected virtual IEnumerator CoRhythmCountdown() {
        while (!gameOver) {
            OnNumberChanged();
            yield return new WaitForSeconds(rhythm);
        }
    }

    protected virtual void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length;
        if (curNumberInd == numbers.Length - 1)
            isFirstRound = false;

        DisplayNumber();
        PlayRythmClick();
    }

    private void OnNumberChosen() {
        gameOver = true;

        Debug.Log("The chosen number is: " + numbers[curNumberInd]);
        // TODO: Display chosen number in some way.
        // TODO: Go to next scene.
    }

    #region UI Methods
    protected void DisplayNumber() {
        numberText.text = numbers[curNumberInd].ToString();
    }

    protected void DisplayEmpty() {
        numberText.text = "";
    }
    #endregion

    #region Sound Methods
    protected virtual void PlayRythmClick() {
        audioSource.PlayOneShot(rythmClickClip);
    }
    #endregion
}
