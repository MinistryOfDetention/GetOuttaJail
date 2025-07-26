using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;

    public float initialWaitTime = 1f;

    public float defaultSentenceWaitTime = 1.5f;
    public float sentenceWaitTime = 0;

    public GameObject[] speakers;
    public GameObject currentSpeaker;

    public Dictionary<int, int> speakerOverride;

    public int speakerIndex;
    public bool dialogueEnd = false;

    private void Start()
    {
        if (sentences.Length <= 0) return;

        // Position text above speakers
        // textDisplay.alignment = TextAlignmentOptions.Midline;
        speakerIndex = 0;
        // textDisplay.rectTransform.position = Camera.main.WorldToScreenPoint(speakers[speakerIndex].position + new Vector3(0, 1, 0));
        currentSpeaker = speakers[speakerIndex].transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (dialogueEnd == true || sentences.Length <= 0)
        {
            return;
        }

        if (initialWaitTime > 0)
        {
            initialWaitTime -= Time.deltaTime;

            if (initialWaitTime <= 0)
            {
                StartCoroutine(Type());
            }
        }
        
        if (initialWaitTime <= 0 && sentenceWaitTime > 0) 
        {
            sentenceWaitTime -= Time.deltaTime;

            // If enough time has passed, go to the next sentence
            if (sentenceWaitTime <= 0)
            {
                NextSentence();
            }
        }
    }

    IEnumerator Type()
    {

        foreach (char letter in sentences[index].ToCharArray())
        {
            string currentDialogueText = currentSpeaker.GetComponent<Dialogue>().GetDialogue();
            currentDialogueText += letter;
            currentSpeaker.GetComponent<Dialogue>().ModifyDialogue(currentDialogueText);
            
            if (dialogueEnd == true)
            {
                EndDialogue();
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(0.02f);
            }
            
        }

        sentenceWaitTime = defaultSentenceWaitTime;
    }

    public void NextSentence()
    {
        if (dialogueEnd == true)
        {
            return;
        }

        currentSpeaker.GetComponent<Dialogue>().ModifyDialogue("");

        // Increase speaker index
        speakerIndex++;
        if (speakerIndex >= speakers.Length)
        {
            // Cycle to start of speaker array
            speakerIndex = 0;
        }

        // Position text on next speaker
        // textDisplay.rectTransform.position = Camera.main.WorldToScreenPoint(speakers[speakerIndex].position  + new Vector3(0, 1, 0));

        currentSpeaker = speakers[speakerIndex].transform.GetChild(1).gameObject;

        if (index < sentences.Length - 1)
        {
            index++;
            currentSpeaker.GetComponent<Dialogue>().ModifyDialogue("");
            StartCoroutine(Type());
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        foreach (GameObject speaker in speakers)
        {
            speaker.transform.GetChild(1).gameObject.GetComponent<Dialogue>().ModifyDialogue("");
        }

        dialogueEnd = true;
    }
}