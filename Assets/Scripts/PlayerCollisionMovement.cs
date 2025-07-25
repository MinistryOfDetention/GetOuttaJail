using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical);

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
