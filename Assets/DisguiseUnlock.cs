using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class DisguiseUnlock : MonoBehaviour
{
    public bool isFirstMask = true;

    public float firstMaskTimer = 7.5f;

    public bool isEggMask = false;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (isFirstMask)
        {
            sr.enabled = false;
        }
    }

    private void Update() {
        if (isFirstMask)
        {
            firstMaskTimer -= Time.deltaTime;
            if (firstMaskTimer <= 0)
            {
                sr.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isEggMask)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (isFirstMask)
            {
                LevelMaster.tutorialDialogueStarted = true;
            }
            Disguise disguise = other.gameObject.GetComponent<Disguise>();
            LevelMaster.numberOfUnlockedDisguises++;
            Destroy(gameObject);


            if (LevelMaster.numberOfUnlockedDisguises == 1)
            {
                LevelMaster.ShowControls();
            }

        }
    }

}
