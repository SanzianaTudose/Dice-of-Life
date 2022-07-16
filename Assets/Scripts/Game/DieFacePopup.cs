using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles movement and animation
public class DieFacePopup : MonoBehaviour {
    [Header("Properties")]
    [SerializeField] private float animTime;
    [SerializeField] private float delay;

    [Header("Dependencies")]
    [SerializeField] private GameObject dieFace;
    [SerializeField] private GameObject sparkles;
    private Image sparklesImage;
    [SerializeField] private Sprite[] dieFaceSprites;

    public void Initialize(int finalNumber) {
        // Set correct die face sprite
        dieFace.GetComponent<Image>().sprite = dieFaceSprites[finalNumber - 1];

        // Animate {dieFace} scale
        dieFace.transform.localScale = Vector3.zero;
        LeanTween.scale(dieFace, Vector3.one, animTime)
                 .setEase(LeanTweenType.easeOutBack)
                 .setDelay(delay); ;

        // Animate {sparkles} sprite alpha
        sparklesImage = sparkles.GetComponent<Image>();
        sparklesImage.color = new Color(sparklesImage.color.r, sparklesImage.color.g, sparklesImage.color.b, 0f);
        LeanTween.value(sparkles, updateAlphaCallback, 0f, 1f, animTime * 0.5f).setDelay(delay);

        LeanTween.value(sparkles, updateAlphaCallback, 1f, 0f, animTime * 0.5f).setDelay(delay + animTime * 1.5f);

        // TODO: Add SFX!
    }

    private void updateAlphaCallback(float val) {
        sparklesImage.color = new Color(sparklesImage.color.r, sparklesImage.color.g, sparklesImage.color.b, val);
    }
}
