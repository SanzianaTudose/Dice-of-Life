using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for the Count Guess game in Scene VI: Slot machine mechanics
// Handles Logic & Input
public class CountGuessSixth : MonoBehaviour {
    public static event Action StartGame = delegate { };
    public static event Action StopGame = delegate { };
    private bool gameStarted = false;

    [SerializeField] private Row[] rows;
    private int finalNumber;

    #region UI Vars
    [SerializeField] private GameObject diePopupPrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Vector3 diePopupPos;
    #endregion

    private void Update() {
        // Handle player input
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameStarted) {
                gameStarted = true;
                StartGame();
                
                return;
            }

            StopGame();
            
            DetermineFinalNumber();
            DisplayDiePopup();
        }
    }

    private void DetermineFinalNumber() {
        string finalString = rows[0].finalSymbol + rows[1].finalSymbol;

        switch (finalString) {
            case "I":
                finalNumber = 1;
                break;
            case "II":
                finalNumber = 2;
                break;
            case "IV":
                finalNumber = 4;
                break;
            case "V":
                finalNumber = 5;
                break;
            case "VI":
                finalNumber = 6;
                break;
            default:
                finalNumber = 1;
                break;
        }
    }

    private void DisplayDiePopup() {
        GameObject popup = Instantiate(diePopupPrefab, diePopupPos, Quaternion.identity);
        popup.transform.SetParent(canvas.transform, false);
        popup.GetComponent<DieFacePopup>().Initialize(finalNumber);
    }
}
