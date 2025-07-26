using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float previousHorizontal;
    private float previousVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool horizontalChanged = horizontal != previousHorizontal;
        bool verticalChanged = vertical != previousVertical;

        if (Mathf.Abs(horizontal) > 0 && Mathf.Abs(vertical) > 0)
        {
            if (horizontalChanged && !verticalChanged)
            {
                movement = new Vector2(horizontal, 0);
            }
            else if (verticalChanged && !horizontalChanged)
            {
                movement = new Vector2(0, vertical);
            }
            else if (horizontalChanged && verticalChanged)
            {
                if (movement.x != 0)
                {
                    movement = new Vector2(horizontal, 0);
                }
                else
                {
                    movement = new Vector2(0, vertical);
                }
            }
        }
        else if (Mathf.Abs(horizontal) > 0)
        {
            movement = new Vector2(horizontal, 0);
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            movement = new Vector2(0, vertical);
        }
        else
        {
            movement = Vector2.zero;
        }

        previousHorizontal = horizontal;
        previousVertical = vertical;
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
