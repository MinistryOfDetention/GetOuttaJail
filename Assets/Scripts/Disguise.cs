using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{

    // List of unlocked disguises
    public string[] unlockedDisguises = new string[] { "None", "Cleaner", "Teacher" };

    public int currentDisguise = 0;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SwitchDisguise();
        }
    }

    void SwitchDisguise()
    {
        currentDisguise++;

        if (currentDisguise >= unlockedDisguises.Length)
        {
            currentDisguise = 0;
        }
    }

}
