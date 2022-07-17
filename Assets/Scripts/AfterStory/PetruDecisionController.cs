using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetruDecisionController : MonoBehaviour {
    private const string FINAL_ROLL = "FINAL_ROLL";
    private int finalRoll;

    #region UI Vars
    [SerializeField] private GameObject dicePopupPrefab;
    [SerializeField] private GameObject dicePopupParent;
    [SerializeField] private Vector3 dicePopupPos;
    #endregion

    private void Start() {
        finalRoll = PlayerPrefs.GetInt(FINAL_ROLL);
    }

    public void DisplayDicePopup() {
        GameObject popup = Instantiate(dicePopupPrefab, dicePopupPos, Quaternion.identity);
        popup.transform.SetParent(dicePopupParent.transform, false);
        popup.transform.localScale = Vector3.one * 0.6f;
        popup.GetComponent<DieFacePopup>().Initialize(finalRoll);
    }
}
