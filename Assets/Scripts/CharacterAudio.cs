using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip maskUnlockSound;
    public AudioClip[] maskSwapSounds;
    public AudioClip doorOpenSound;
    public AudioClip detectionSound;

    public void PlayClip(String clipName)
    {
        AudioClip clip = null;
        switch (clipName)
        {
            case "footsteps":
                clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
                break;
            case "maskUnlock":
                clip = maskUnlockSound;
                break;
            case "maskSwap":
                clip = maskSwapSounds[UnityEngine.Random.Range(0, maskSwapSounds.Length)];
                break;
            case "doorOpen":
                clip = doorOpenSound;
                break;
            case "detection":
                clip = detectionSound;
                break;
        }

        if (clip != null)
        {
            AudioManager.Instance.PlaySFX(clip);
        }
    }
}
