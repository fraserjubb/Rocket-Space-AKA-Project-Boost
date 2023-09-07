using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject currentLevelBox;

    private int currentSceneIndex;

    public static bool gameIsPaused = false;
    public static bool onSecondaryPauseMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && DialogueManager.initialTutorialIsRunning == false && onSecondaryPauseMenu == false)
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        currentLevelBox.SetActive(true);
        Time.timeScale = 1f; // Allows for gameplay to pass at a normal rate
        gameIsPaused = false;
        onSecondaryPauseMenu = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        currentLevelBox.SetActive(false);
        Time.timeScale = 0f; // Completely freezes gameplay
        gameIsPaused = true;
    }

    public void Confirmation()
    {
        onSecondaryPauseMenu = true; // Needed to allow pause button to function properly when P is pressed
    }

    public void Options()
    {
        onSecondaryPauseMenu = true;
    }

    public void ReturnToPause()
    {
        onSecondaryPauseMenu = false;
    }

    public void ReturnToMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        Resume();
        SceneManager.LoadScene("Main Menu");
    }
    
}
