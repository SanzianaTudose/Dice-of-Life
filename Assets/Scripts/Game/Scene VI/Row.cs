using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to rows in Scene VI 
// Controls movement
public class Row : MonoBehaviour {
    private const float EPS = 0.4f;

    [SerializeField] private float rotInterval;
    [SerializeField] private float posUpperY, posLowerY;
    [SerializeField] private float rotSpeed;
    [SerializeField] private int rotDir;
    
    public bool rowStopped;
    public string curSlotItem;

    private void Start() {
        rowStopped = true;
        CountGuessSixth.StartGame += StartRotating;
        CountGuessSixth.StopGame += StopRotating;
    }

    private void StartRotating() {
        curSlotItem = "";
        StartCoroutine(CoRotate());
    }

    private void StopRotating() {
        rowStopped = true;
        // TODO: Determine stop position so it's centered
    }

    private IEnumerator CoRotate() {
        rowStopped = false;

        // One loop gives one rotation
        while(!rowStopped) {
            // When row reaches upper end, immediately shift it to lower end
            // => Creates rotating illusion if both sides have the same symbol
            if (Mathf.Abs(transform.position.y - posUpperY) <= EPS) 
                transform.position = new Vector2(transform.position.x, posLowerY);

            // Translate Row downwards by a set amount 
            transform.position = new Vector2(transform.position.x, transform.position.y - rotDir * rotSpeed);  

            yield return new WaitForSeconds(rotInterval);
        }
    }
}
