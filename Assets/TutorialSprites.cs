using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TutorialSprites : MonoBehaviour
{
    public GameObject spritesObject;
    public float delayShowSprites = 30f;

    void Start()
    {
        spritesObject.SetActive(false);
        // StartCoroutine(ShowSpritesDelay(delayShowSprites));
    }

    private void Update() {
        if (LevelMaster.tutorialDialogueStarted && spritesObject != null)
        {
            spritesObject.SetActive(true);
        }
    }
    
    // IEnumerator ShowSpritesDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     spritesObject.SetActive(true);
    // }

}
