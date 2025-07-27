using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chris : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject eggMask;

    private Boolean maskFalling = false;
    private float vel = 0.0f;
    private float acc = 0.000015f;
    void Start()
    {
        eggMask.transform.position = transform.position + Vector3.up * 0.7f;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (maskFalling)
        {
            vel -= acc;
            eggMask.transform.position += Vector3.up * vel;

            if (eggMask.transform.position.y < transform.position.y - 1.5)
            {
                maskFalling = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pen projectile") && collision.transform.up.y - Vector3.down.y < 0.1f)
        {
            collision.GetComponent<PenProjectile>().drop();
            //Trigger mask falling off.
            maskFalling = true;
            vel = 0.01f;
        }
    }
}
