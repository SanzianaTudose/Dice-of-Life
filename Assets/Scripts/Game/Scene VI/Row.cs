using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to rows in Scene VI 
// Controls movement
public class Row : MonoBehaviour {
    private const float EPS = 0.4f;
    private const float MAX_DIST = 100f;

    [SerializeField] private float rotInterval;
    [SerializeField] private float posUpperY, posLowerY;
    [SerializeField] private float rotSpeed;
    [SerializeField] private int rotDir;
    [SerializeField] private float[] symbolPosY; 
    [SerializeField] private string[] symbolString; 
    
    public bool rowStopped;
    public string finalSymbol;
    
    private int closestInd;

    private void Start() {
        rowStopped = true;
        CountGuessSixth.StartGame += StartRotating;
        CountGuessSixth.StopGame += StopRotating;
    }

    private void StartRotating() {
        finalSymbol = "";

        StartCoroutine(CoRotate());
    }

    private void StopRotating() {
        rowStopped = true;

        FindClosestSymbol();
        MoveToClosestSymbol();
    }

    private IEnumerator CoRotate() {
        rowStopped = false;

        // One loop gives one rotation
        while(!rowStopped) {
            RotateOnce();

            yield return new WaitForSeconds(rotInterval);
        }
    }

    private void RotateOnce() {
        // When row reaches upper end, immediately shift it to lower end
        // => Creates rotating illusion if both sides have the same symbol
        if (Mathf.Abs(transform.position.y - posUpperY) <= EPS)
            transform.position = new Vector2(transform.position.x, posLowerY);

        // Translate Row downwards by a set amount 
        transform.position = new Vector2(transform.position.x, transform.position.y - rotDir * rotSpeed);
    }

    #region Stop Rotating Methods
    private void FindClosestSymbol() {

        // Find symbol that is closest to the stopped position
        float minDist = MAX_DIST;
        closestInd = -1;
        for (int i = 0; i < symbolPosY.Length; i++) {
            float dist = Mathf.Abs(transform.position.y - symbolPosY[i]);

            if (dist < minDist) {
                minDist = dist;
                closestInd = i;
            }
        }

        finalSymbol = symbolString[closestInd];
    }

    private void MoveToClosestSymbol() {
        LeanTween.moveY(gameObject, symbolPosY[closestInd], 0.5f);
    }
    #endregion
}
