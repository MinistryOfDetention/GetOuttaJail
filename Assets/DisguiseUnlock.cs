using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseUnlock : MonoBehaviour
{

    private void Start() {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Disguise disguise = other.gameObject.GetComponent<Disguise>();
            LevelMaster.numberOfUnlockedDisguises++;
            Destroy(gameObject);
        }
    }

}
