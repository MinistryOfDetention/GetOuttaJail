using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PenProjectile : MonoBehaviour

{

    public float speed;
    public GameObject itemDrop;

    private float bounceBack = 0.5f; // The degree to which an item drop will spawn away from the collision.

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
        itemDrop.transform.position = transform.position + (transform.up * -bounceBack);
        itemDrop.SetActive(true);
    }
}
