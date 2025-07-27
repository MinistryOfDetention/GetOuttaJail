using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class DisguiseUnlock : MonoBehaviour
{
    public bool isEggMask = false;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isEggMask)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
