using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetruDecisionController : MonoBehaviour {
    private int finalRoll;

    #region UI Vars
    [SerializeField] private GameObject dicePopupPrefab;
    [SerializeField] private GameObject dicePopupParent;
    [SerializeField] private Vector3 dicePopupPos;
    #endregion

    private void Start() {
        DetermineRoll();
    }

    private void DetermineRoll() {
        // TODO: determine roll based on Dice of Life
        finalRoll = 6;
    }
    public void DisplayDicePopup() {
        GameObject popup = Instantiate(dicePopupPrefab, dicePopupPos, Quaternion.identity);
        popup.transform.SetParent(dicePopupParent.transform, false);
        popup.transform.localScale = Vector3.one * 0.6f;
        popup.GetComponent<DieFacePopup>().Initialize(finalRoll);
    }
}
