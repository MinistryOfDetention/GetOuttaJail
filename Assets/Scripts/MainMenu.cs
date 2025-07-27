using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{

    public RectTransform CreditPanel;
    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("bathroom");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void ShowCredits()
    {
        // Show the credits panel
        CreditPanel.gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        // Hide the credits panel
        CreditPanel.gameObject.SetActive(false);
    }
}
