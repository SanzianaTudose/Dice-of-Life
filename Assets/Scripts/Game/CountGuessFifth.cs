using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene V
// Additions: roman numeral sticks are displayed one by one and correspond to different numbers
//            numbers do not display after 2
public class CountGuessFifth : CountGuessManager {
    [SerializeField] protected string[] displayedNums;

    // Do not display numbers after 3
    protected override void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length;
        if (curNumberInd == numbers.Length - 1)
            isFirstRound = false;

        PlayRythmClick();
        // First 3 array elements correspond to numbers 1 and 2
        if (curNumberInd == 0 || (curNumberInd < 3 && isFirstRound))
            DisplayNumber();
        else
            DisplayEmpty();
    }

    protected override void DisplayNumber() {
        numberText.text = displayedNums[curNumberInd];
    }
}
