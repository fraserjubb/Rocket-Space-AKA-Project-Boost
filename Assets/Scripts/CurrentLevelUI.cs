using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrentLevelUI : MonoBehaviour
{
    public TMP_Text currentLevelTextUI;
    Scene currentScene;
    int finalScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        finalScene = SceneManager.sceneCountInBuildSettings;
        currentLevelTextUI.text = ($"Level: {currentScene.buildIndex} / {finalScene - 1}");
    }
}
