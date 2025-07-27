using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{   
    public DialogueManager dm;

    public AudioClip pickUpSound; // The item to drop when the player collides with this object.
    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                //Debug.Log("Item collision detected with: " + other.gameObject.name);
                if (pickUpSound != null)
                {
                    AudioManager.Instance.PlaySFX(pickUpSound);
                }
                inventory.AddItem(gameObject);
                gameObject.SetActive(false);

                if (!LevelMaster.penTutorialStarted)
                {
                    dm.AddDialogue(new string[] {
                        "A pen? I can use the SHIFT key to launch it at teachers!",
                        "I'll need to drop the pen when I'm entering a new room though..."
                    });
                    LevelMaster.penTutorialStarted = true;
                }

            }
        }
        else
        {
            //Debug.Log("Item collision with non-player object: " + other.gameObject.name);
        }
    }
}
