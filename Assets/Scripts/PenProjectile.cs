using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PenProjectile : MonoBehaviour

{

    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        transform.position = transform.position + transform.up * speed * Time.fixedDeltaTime;
    }
    void Update()
    {
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
