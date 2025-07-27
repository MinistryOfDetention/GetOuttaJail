using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{

    // List of unlocked disguises
    public string[] unlockedDisguises = new string[] { "None", "Square", "Circle", "Triangle", "Egg" };
    public Sprite[] disguiseSprites;
    public SpriteRenderer disguiseRenderer;

    public int currentDisguise = 0;

    public CharacterAudio characterAudio;

    void Start()
    {
        Debug.Log(unlockedDisguises);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SwitchDisguise();
            characterAudio.PlayClip("maskSwap");
        }
    }

    void SwitchDisguise()
    {
        currentDisguise++;

        if (currentDisguise > LevelMaster.numberOfUnlockedDisguises)
        {
            currentDisguise = 0;
        }

        if (currentDisguise <= LevelMaster.numberOfUnlockedDisguises)
        {
            disguiseRenderer.sprite = disguiseSprites[currentDisguise];
        }
        else
        {
            Debug.LogWarning("No sprite assigned for current disguise index: " + currentDisguise);
        }
        Debug.Log("Switched to disguise: " + unlockedDisguises[currentDisguise]);
    }

}
