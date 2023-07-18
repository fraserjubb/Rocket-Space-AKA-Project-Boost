using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    // public void Options()
    // {
    //     Debug.Log("I am the options");
    // }

    public void QuitGame()
    {
        Debug.Log("Game has been quit");
        Application.Quit();
    }
}
