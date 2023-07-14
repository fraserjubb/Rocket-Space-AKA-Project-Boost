using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI; // Allows for game object to be applied in unity (similar to Serialize Field)

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
        Time.timeScale = 1f; // Allows for gameplay to pass at a normal rate
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Completely freezes gameplay
        gameIsPaused = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // AreYouSure
    // confirmation.SetActive()
    // if yes > returnToMenu()
    // if no > back to Pause()

}
