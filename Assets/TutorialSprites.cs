using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TutorialSprites : MonoBehaviour
{
    public GameObject spritesObject;
    public float delayShowSprites = 10f;

    void Start()
    {   
        spritesObject.SetActive(false);
        StartCoroutine(ShowSpritesDelay(delayShowSprites));
    }
    
    IEnumerator ShowSpritesDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spritesObject.SetActive(true);
    }

}
