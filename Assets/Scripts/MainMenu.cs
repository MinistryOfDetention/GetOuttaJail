using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("classroom");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
