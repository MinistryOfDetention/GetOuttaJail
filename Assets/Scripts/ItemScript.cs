using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                //Debug.Log("Item collision detected with: " + other.gameObject.name);
                inventory.AddItem(gameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            //Debug.Log("Item collision with non-player object: " + other.gameObject.name);
        }
    }
}
