using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public void AddItem(GameObject item)
    {
        items.Add(item);
        //Debug.Log("Item added: " + item);
        // print the current inventory
        //Debug.Log("Current Inventory: ");
        foreach (GameObject i in items)
        {
            //Debug.Log(i.name);
        }
    }

    public GameObject RemoveItem(String tag)
    {
        foreach (GameObject i in items)
        {
            
            if (i.tag == tag)
            {
                items.Remove(i);
                return i;
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
