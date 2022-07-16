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

    [SerializeField] private GameObject dicePopupPrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Vector3 dicePopupPos;
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

    protected int finalNumber;

    private void Awake() {
        DisplayEmpty();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update() {
        // Handle player input
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameStarted) 
                StartGame();
            else if (!gameOver) 
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

        finalNumber = numbers[curNumberInd];
        DisplayDicePopup();

        // TODO: Keep track of final numbers for the final die!
    }

    #region UI Methods
    protected virtual void DisplayNumber() {
        numberText.text = numbers[curNumberInd].ToString();
    }

    protected void DisplayEmpty() {
        numberText.text = "";
    }

    private void DisplayDicePopup() {
        GameObject popup = Instantiate(dicePopupPrefab, dicePopupPos, Quaternion.identity);
        popup.transform.SetParent(canvas.transform, false);
        popup.GetComponent<DieFacePopup>().Initialize(finalNumber);
    }
    #endregion

    #region Sound Methods
    protected virtual void PlayRythmClick() {
        audioSource.PlayOneShot(rythmClickClip);
    }
    #endregion
}
