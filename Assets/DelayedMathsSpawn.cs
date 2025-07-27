using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedMathsSpawn : MonoBehaviour
{

    public GameObject teacherSpawn;
    public float spawnWaitTime = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (spawnWaitTime > 0)
        {
            spawnWaitTime -= Time.deltaTime;
            if (spawnWaitTime < 0)
            {
                teacherSpawn.SetActive(true);
            }
        }
    }
}
