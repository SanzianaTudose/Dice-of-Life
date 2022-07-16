using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Base class for all Count Guess games in different scenes
// FOR NOW: Handles everything! Logic, UI, Sound & Input
public class CountGuessManager : MonoBehaviour {

    [SerializeField] protected float rhythm;
    [SerializeField] protected int[] numbers;

    // Game state vars
    private bool gameStarted = false;
    private bool gameOver = false;

    protected int curNumberInd;

    #region UI Vars
    [SerializeField] protected TMP_Text numberText;
    #endregion

    private void Awake() {
        numberText.text = "";
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
        StartCoroutine(RhythmCountdown());  
    }

    protected virtual IEnumerator RhythmCountdown() {
        while (!gameOver) {
            OnNumberChanged();
            yield return new WaitForSeconds(rhythm);
        }
    }

    protected virtual void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length; 
        
        DisplayNumber();
        // TODO: Play rhythm sound.
    }

    private void OnNumberChosen() {
        gameOver = true;

        Debug.Log("The chosen number is: " + numbers[curNumberInd]);
    }

    #region UI Methods
    private void DisplayNumber() {
        numberText.text = numbers[curNumberInd].ToString();
    }
    #endregion

    #region Sound Methods
        // TODO: Add method to play rythm sound.
    #endregion
}
