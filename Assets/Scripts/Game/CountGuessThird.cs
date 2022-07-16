using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene III
// Additions: faster rhythm, numbers do not display after 1
public class CountGuessThird : CountGuessManager {
    protected override void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length;

        PlayRythmClick();
        if (curNumberInd < 1)
            DisplayNumber();
        else
            DisplayEmpty();
    }
}
