using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene IV
// Additions: numbers do not display after 3, click rhythm is randomized
public class CountGuessFourth : CountGuessManager {
    [SerializeField] private Vector2 clickModifierRange;

    // Start CoRhythmSound when CoRhythmCountdown() is called
    protected override IEnumerator CoRhythmCountdown() {
        StartCoroutine(CoRhythmClick());

        while (!gameOver) {
            OnNumberChanged();
            yield return new WaitForSeconds(rhythm);
        }
    }

    // Do not call PlayRythmClick() when number changes
    // Do not display numbers after 3
    protected override void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length;
        if (curNumberInd == numbers.Length - 1)
            isFirstRound = false;

        if (curNumberInd == 0 || (curNumberInd < 3 && isFirstRound))
            DisplayNumber();
        else
            DisplayEmpty();
    }

    private IEnumerator CoRhythmClick() {
        while (!gameOver) {
            PlayRythmClick();

            // Randomly change waiting time in-between clicks
            float modifier = Random.Range(clickModifierRange.x, clickModifierRange.y);
            float clickRhythm = rhythm * modifier;
            yield return new WaitForSeconds(clickRhythm);
        }
    }
}
