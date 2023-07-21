using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//NOTE: This class is currently used on both the Main Menu & Game Complete scenes.
public class MainMenu : MonoBehaviour
{
    public void PlayGame() // When play button is pressed on the main menu
    {
        SceneManager.LoadScene(1); // Will load specified scene
    }

    // public void Options()
    // {
    //     // Currently controlled through Unity inspector
    // }

    public void QuitGame() // When quit button in pressed on the main menu
    {
        Debug.Log("Game has been quit"); // To show that game is being quit
        Application.Quit(); // Only works in final build
    }
}
