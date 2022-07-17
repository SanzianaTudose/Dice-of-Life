using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene VII: Random number
public class CountGuessSeventh : MonoBehaviour {
    private int finalNumber;

    private bool gameOver = false;

    #region UI Vars
    [SerializeField] private GameObject dicePopupPrefab;
    [SerializeField] private GameObject dicePopupParent;
    [SerializeField] private Vector3 dicePopupPos;
    #endregion

    private void Update() {
        // Handle player input
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameOver) {
                gameOver = true;

                DetermineFinalNumber();
                DisplayDicePopup();
                // TODO: Keep track of final numbers for the final die!
            }
        }
    }

    private void DetermineFinalNumber() {
        finalNumber = Random.Range(1, 7);
    }

    private void DisplayDicePopup() {
        GameObject popup = Instantiate(dicePopupPrefab, dicePopupPos, Quaternion.identity);
        popup.transform.SetParent(dicePopupParent.transform, false);
        popup.transform.localScale = Vector3.one;
        popup.GetComponent<DieFacePopup>().Initialize(finalNumber);
    }
}
