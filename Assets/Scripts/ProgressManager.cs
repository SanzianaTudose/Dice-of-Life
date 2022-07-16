using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads and manages Scenes
public class ProgressManager : MonoBehaviour {
    // Set to true when next Scene can be loaded
    private bool canProgress;

    [SerializeField] private GameObject canProgressText;

    private void Awake() {
        SetCanProgress(false);

        // TODO: For now, Story Scenes can be skipped without doing anything. REMOVE THIS!
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

        // TODO: if (canProgress) => display text on the bottom "Press Spacebar to continue..."
        canProgressText.SetActive(canProgress);
    }

    public void LoadNextScene() {
        if (!canProgress) return;

        Scene curScene = SceneManager.GetActiveScene();

        if (curScene.buildIndex + 1 >= SceneManager.sceneCountInBuildSettings) {
            Debug.LogError("ProgressManager: There's no next scene!");
            return;
        }

        SceneManager.LoadScene(curScene.buildIndex + 1);
    }

}
