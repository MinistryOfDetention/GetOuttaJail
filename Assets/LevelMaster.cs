using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static int numberOfUnlockedDisguises = 0;

    void Start()
    {
        LevelMaster.numberOfUnlockedDisguises = 0;
    }
}
