using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI; // Allows for game object to be applied in unity (similar to Serialize Field)
    public GameObject confirmationMenuUI;
    public GameObject currentLevelBox;


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
        currentLevelBox.SetActive(true);
        Time.timeScale = 1f; // Allows for gameplay to pass at a normal rate
        gameIsPaused = false;
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
        pauseMenuUI.SetActive(false);
        confirmationMenuUI.SetActive(true);
    }

    public void ReturnToPause()
    {
        confirmationMenuUI.SetActive(false);        
        pauseMenuUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Resume();
        SceneManager.LoadScene("Main Menu");
    }
}
