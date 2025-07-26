using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Dialogue : MonoBehaviour
{
    public float defaultShowTime = 3f;

    float showTime = 0;

    TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrement show time
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;

            // If exceeded, disable dialog text
            if (showTime <= 0)
            {
                tmp.text = "";
            }
        }
    }

    public void DisplayDialogue(string text)
    {
        tmp.text = text;
        showTime = defaultShowTime;
    }

    public void ModifyDialogue(string text)
    {
        tmp.text = text;
    }

    public string GetDialogue()
    {
        return tmp.text;
    }
}
