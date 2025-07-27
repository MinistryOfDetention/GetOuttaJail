using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoorCollision : MonoBehaviour
{
    public DialogueManager dm;

    public bool isEndgame = false;

    private void Start()
    {
        dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isEndgame)
            {
                // Trigger locked door dialogue.
                //Debug.Log("Door is locked...");
                dm.AddDialogue(new string[] {
                "Hmm... the door's locked.",
                "I'll need an Egg mask for this door.",
                "Maybe the principal will have the Egg mask..."
            });
                LevelMaster.playerInDialogue = true;
            }
            else
            {
                dm.AddDialogue(new string[] {
                    "The Egg mask worked! I'm free!!"
                });
                LevelMaster.inEndgameDialogue = true;
            }
        }
    }
}
