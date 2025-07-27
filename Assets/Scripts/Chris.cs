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
        eggMask.GetComponent<BoxCollider2D>().enabled = false;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (maskFalling)
        {
            vel -= acc;
            eggMask.transform.position += Vector3.up * vel;
            eggMask.transform.position += Vector3.right * 0.001f;

            if (eggMask.transform.position.y < transform.position.y - 1.5)
            {
                maskFalling = false;
                eggMask.GetComponent<BoxCollider2D>().enabled = true;
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
            vel = 0.007f;
        }
    }
}
