using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderController : MonoBehaviour {
    private const string FACE_COUNT = "FACE_COUNT";
    private const string FACE_VAL = "FACE_VAL";

    void Start() {
        int count = PlayerPrefs.GetInt(FACE_COUNT);

        Debug.Log(count);
        for (int i = 1; i <= count; i++) {
            int face = PlayerPrefs.GetInt(FACE_VAL + i.ToString());

            Debug.Log("Face " + i + ": " + face);
        }
    }
}
