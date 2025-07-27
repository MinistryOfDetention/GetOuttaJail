using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoorCollision : MonoBehaviour
{
    public DialogueManager dm;

    private void Start()
    {
        dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger locked door dialogue.
            //Debug.Log("Door is locked...");
            dm.AddDialogue(new string[] {
                "Hmm... the door's locked.",
                "I'll need a key for this door.",
                "Maybe the principal will have the key..."
            });
            LevelMaster.playerInDialogue = true;
        }
    }
}
