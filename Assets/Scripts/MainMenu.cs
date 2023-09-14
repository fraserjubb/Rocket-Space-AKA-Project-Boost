using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//NOTE: This class is currently used on both the Main Menu & Game Complete scenes.
public class MainMenu : MonoBehaviour
{
    public GameObject confirmationText;
    public GameObject mainMenuUI;

    private int sceneToContinue;

    public void NewGame() // When play button is pressed on the main menu
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        
        if (sceneToContinue > 1)
        {    
            mainMenuUI.SetActive(false);
            confirmationText.SetActive(true);
        }
        else
            SceneManager.LoadScene(1); // Will load specified scene
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);        
    }

    // public void Options()
    // {
    //     // Currently controlled through Unity inspector
    // }

    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue!=0)
            SceneManager.LoadScene(sceneToContinue);
        else
            SceneManager.LoadScene(1);
    }

    public void QuitGame() // When quit button in pressed on the main menu
    {
        Debug.Log("Game has been quit"); // To show that game is being quit
        Application.Quit(); // Only works in final build
    }
}
