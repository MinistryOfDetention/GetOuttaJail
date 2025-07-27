using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{

    public static int numberOfUnlockedDisguises = 0;

    public static bool playerInDialogue = false;

    static bool tutorialDialogueStarted = false;

    static bool controlsTutorialStarted = false;

    public static bool penTutorialStarted = false;

    public static bool inEndgameDialogue = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LevelMaster.numberOfUnlockedDisguises = 0;
    }

    public static void ShowControls()
    {
        DialogueManager dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        if (dm)
        {
            dm.AddDialogue(new string[] {
                "Ah, I can press SPACEBAR to disguise myself as a teacher.",
                "Time to get out of this crusty old school!"
            });
        }
    }
}
