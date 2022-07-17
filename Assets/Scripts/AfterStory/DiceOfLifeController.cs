using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceOfLifeController : MonoBehaviour {
    [SerializeField] private float waitTime;

    [Header("Line Animation Properties")]
    [SerializeField] private GameObject lines;
    [SerializeField] private float colAnimTime;
    [SerializeField] private float scaleAnimTime;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;

    private Image linesImage;

    private ProgressManager progressManager;

    private void Start() {
        progressManager = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();

        linesImage = lines.GetComponent<Image>();

        AnimateLines();
        StartCoroutine(CoCanProgress());
    }

    private void AnimateLines() {
        LeanTween.value(lines, UpdateColorCallback, firstColor, secondColor, colAnimTime)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setLoopPingPong();

        LeanTween.scale(lines, Vector3.one * scaleFactor, scaleAnimTime)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setLoopPingPong(); 
    }

    private void UpdateColorCallback(Color color) {
        linesImage.color = color;
    }

    private IEnumerator CoCanProgress() {
        yield return new WaitForSeconds(waitTime);
        progressManager.SetCanProgress(true);
    }
}
