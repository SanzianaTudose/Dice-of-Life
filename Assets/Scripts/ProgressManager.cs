using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads and manages Scenes
// Handles UI for Scene Transition
public class ProgressManager : MonoBehaviour {
    // Set to true when next Scene can be loaded
    private bool canProgress;

    #region UI Vars
    [SerializeField] private float fadeAnimTime;

    private GameObject canProgressText;
    private GameObject transitionPanel;
    #endregion

    private void Start() {
        canProgressText = GameObject.Find("ProgressText");
        transitionPanel = GameObject.Find("SceneTransitionPanel");

        SetCanProgress(false);
        FadeIn();

        // TODO: For now, Story Scenes can be skipped without doing anything.
        // REMOVE THIS when DialogueManager is a thing!
        Scene curScene = SceneManager.GetActiveScene();
        int buildInd = curScene.buildIndex;
        if (buildInd % 2 == 1 || buildInd == 0)
            SetCanProgress(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadNextScene();
    }

    public void SetCanProgress(bool canProgress) {
        this.canProgress = canProgress;

        canProgressText.SetActive(canProgress);
    }

    public void LoadNextScene() {
        if (!canProgress) return;

        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.buildIndex + 1 >= SceneManager.sceneCountInBuildSettings) {
            Debug.LogError("ProgressManager: There's no next scene!");
            return;
        }

        FadeOut(curScene);
    }

    #region UI Methods
    private void FadeIn() {
        CanvasGroup canvasGroup = transitionPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        LeanTween.alphaCanvas(canvasGroup, 0f, fadeAnimTime); // TODO: setEase ?
    }

    private void FadeOut(Scene curScene) {
        CanvasGroup canvasGroup = transitionPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        LeanTween.alphaCanvas(canvasGroup, 1f, fadeAnimTime)
                 .setOnComplete(() => { SceneManager.LoadScene(curScene.buildIndex + 1); }); 
        // TODO: setEase ?
    }
    #endregion

}
