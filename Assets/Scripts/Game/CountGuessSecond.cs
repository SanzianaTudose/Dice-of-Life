using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene II
// Additions: numbers do not display after 3
public class CountGuessSecond : CountGuessManager {
    protected override void OnNumberChanged() {
        curNumberInd = (curNumberInd + 1) % numbers.Length;

        PlayRythmClick();
        if (curNumberInd < 3)
            DisplayNumber();
        else
            DisplayEmpty();
    }
}
