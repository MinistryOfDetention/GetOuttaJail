using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic("sad");
    }
    void Transitition()
    {
        // Load the end game cutscene scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
