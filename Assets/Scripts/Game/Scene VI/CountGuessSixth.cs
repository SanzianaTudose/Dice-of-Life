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

    protected virtual void Update() {
        // Handle player input
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameStarted) {
                gameStarted = true;
                StartGame();
                
                return;
            }

            StopGame();

            gameStarted = false;
        }
    }
}
