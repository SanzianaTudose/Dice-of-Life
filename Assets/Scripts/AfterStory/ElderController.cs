using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderController : MonoBehaviour {
    private const string FACE_COUNT = "FACE_COUNT";
    private const string FACE_VAL = "FACE_VAL";
    private const string LAST_ROLL = "LAST_ROLL";

    private List<int> faces;

    [SerializeField] List<DieFacePopup> die;

    void Start() {
        MakeFacesList();
        RemoveSmallestFace();
    }

    public void StartDiceDisplay() {
        StartCoroutine(CoDisplayDice());
    }

    private IEnumerator CoDisplayDice() {

        for (int i = 0; i < faces.Count; i++) {
            die[i].Initialize(faces[i]);
            yield return null;
        }
    }

    #region Initialize Methods
    private void MakeFacesList() {
        faces = new List<int>();

        int count = PlayerPrefs.GetInt(FACE_COUNT);

        for (int i = 1; i <= count; i++) {
            int face = PlayerPrefs.GetInt(FACE_VAL + i.ToString());
            faces.Add(face);
        }
    }

    private void RemoveSmallestFace() {
        int small = 7, index = -1;
        for (int i = 0; i < faces.Count; i++) 
            if (small > faces[i]) {
                small = faces[i];
                index = i;
            }

        faces.RemoveAt(index);
    }

    private void DetermineFinalRoll() {
        // Determine minimum face

        // 6 => 100%
        // altfel min * 5 daca exista un 6
        // no 6 => hell

        // add it to playerprefs
    }
    #endregion
}
