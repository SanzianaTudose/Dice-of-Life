using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads and manages Scenes
// Handles UI for Scene Transition
public class ProgressManager : MonoBehaviour {
    // Set to true when next Scene can be loaded
    private bool canProgress;
    MusicManager musicManager;

    #region UI Vars
    [SerializeField] private float fadeAnimTime;

    private GameObject canProgressText;
    private GameObject transitionPanel;
    #endregion

    private void Start() {
        canProgressText = GameObject.Find("ProgressText");
        transitionPanel = GameObject.Find("SceneTransitionPanel");
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        SetCanProgress(false);
        FadeIn();
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

        if(curScene.buildIndex==15)
        {
            Debug.Log("scene ending");
            musicManager.ChangeBGM(musicManager.endTheme);
        }
        if (curScene.buildIndex == 19)
        {
            Debug.Log("scene bad end");
            musicManager.ChangeBGM(musicManager.badEndMusic);
        }
        if (curScene.buildIndex == 20)
        {
            Debug.Log("scene good end");
            musicManager.ChangeBGM(musicManager.goodEndMusic);
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
