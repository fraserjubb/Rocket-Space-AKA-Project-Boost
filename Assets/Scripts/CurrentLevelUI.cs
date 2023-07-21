using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrentLevelUI : MonoBehaviour
{
    public TMP_Text currentLevelTextUI; //This allows for directing editing of UI text in-game
    Scene currentScene;
    int finalScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        finalScene = SceneManager.sceneCountInBuildSettings;
        currentLevelTextUI.text = ($"LEVEL: 0{currentScene.buildIndex} of 0{finalScene - 2}");
    }
}
