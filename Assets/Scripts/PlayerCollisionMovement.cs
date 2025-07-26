using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCollisionMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 movement;

    private Animator bodyAnimator;

    void Awake()
    {
        bodyAnimator = GetComponentInChildren<Animator>();

        if (bodyAnimator == null)
        {
            Debug.LogError("Animator component not found on PlayerCollisionMovement script.");
        }
    }
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

        HandleAnimations();

    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void HandleAnimations()
    {
        if (movement.x > 0)
        {
            // Move Right
            bodyAnimator.Play("PlayerMoveRight");
        }
        else if (movement.x < 0)
        {
            // Move Left
            bodyAnimator.Play("PlayerMoveLeft");
        }
        else if (movement.y > 0)
        {
            // Move Up
            bodyAnimator.Play("PlayerMoveUp");
        }
        else if (movement.y < 0)
        {
            // Move Down
            bodyAnimator.Play("PlayerMoveDown");
        }
        else
        {
            // Idle state, you can set an idle animation if needed
            bodyAnimator.Play("PlayerIdle");
        }
    }
}
